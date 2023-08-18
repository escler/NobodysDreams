using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToMatObject : MonoBehaviour
{
    NavMeshAgent nma;
    Thief thief;
    Transform objectPos, actualPos;
    // Start is called before the first frame update

    private void OnDisable()
    {
        thief.StealObject = false;
    }

    void Start()
    {
        thief = GetComponent<Thief>();
        nma = thief.NMA;
    }

    // Update is called once per frame
    void Update()
    {
        if (thief.ObjectPos != null)
        {
            float distance = Vector3.Distance(transform.position, thief.ObjectPos.position);
            if (distance < 1)
            {
                nma.SetDestination(transform.position);
                thief.StealObject = true;
            }
            else
            {

                if (thief.ObjectPos.hasChanged)
                {
                    thief.StealObject = false;
                    nma.SetDestination(thief.ObjectPos.position);
                }
            }
        }
        else
        {
            thief.ObjectToSteal = false;
            this.enabled = false;
        }

    }
}
