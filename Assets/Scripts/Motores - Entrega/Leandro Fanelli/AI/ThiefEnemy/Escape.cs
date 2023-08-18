using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escape : MonoBehaviour
{
    NavMeshAgent nma;
    [SerializeField] Transform[] patrolPoints;
    float distance;
    int currentPoint;
    [SerializeField] float minDistanceToRun;
    Thief thief;

    // Start is called before the first frame update
    void Start()
    {
        thief = GetComponent<Thief>();
        nma = thief.NMA;
    }

    // Update is called once per frame
    void Update()
    {
        distance = thief.Distance;

        if (!nma.pathPending && nma.hasPath && nma.remainingDistance < 0.5f)
        {
            if (currentPoint < patrolPoints.Length - 1)
            {
                currentPoint++;
                nma.SetDestination(patrolPoints[currentPoint].position);
            }
            else
            {
                currentPoint = 0;
                nma.SetDestination(patrolPoints[0].position);
            }
        }

        if (distance > minDistanceToRun)
        {
            nma.SetDestination(transform.position);
        }
        else
        {
            nma.SetDestination(patrolPoints[currentPoint].position);
        }
    }
}
