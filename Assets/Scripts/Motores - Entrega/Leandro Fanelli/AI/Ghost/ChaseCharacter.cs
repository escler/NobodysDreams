using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseCharacter : MonoBehaviour
{
    [SerializeField] MeshRenderer mRen;
    [SerializeField] float minDistanceChar;
    [SerializeField] Material redEyes, whiteEyes;
    Ghost ghost;
    NavMeshAgent nma;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (mRen != null)
        {
            mRen.material = redEyes;
        }
    }

    void Start()
    {
        ghost = GetComponent<Ghost>();
        nma = ghost.NMA;
        if (mRen != null)
        {
            mRen = transform.Find("PM3D_Sphere3D1").gameObject.GetComponent<MeshRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (mRen != null)
        {
            mRen.material = redEyes;
        }

        if (ghost.Distance < minDistanceChar)
        {
            ghost.GhostAttack = true;
            nma.SetDestination(transform.position);
        }
        else
        {
            nma.SetDestination(ghost.CharacterPos.position);
        }
    }
}
