using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//FINAL - Leandro Fanelli - Se hace uso de Herencia para el Slime
public class Slime : Enemies
{
    [SerializeField] Transform[] patrolPoints;
    int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        nma.SetDestination(patrolPoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}

