using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StayPos : MonoBehaviour
{
    Ghost ghost;
    Thief thief;
    NavMeshAgent nma;
    // Start is called before the first frame update
    private void Start()
    {
        if (GetComponent<Ghost>() != null)
        {
            ghost = GetComponent<Ghost>();
            nma = ghost.NMA;
        }
        else
        {
            thief = GetComponent<Thief>();
            nma = thief.NMA;
        }
    }

    // Update is called once per frame
    void Update()
    {
        nma.SetDestination(transform.position);
    }
}
