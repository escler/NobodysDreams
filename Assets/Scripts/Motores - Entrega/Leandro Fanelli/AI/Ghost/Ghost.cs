using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// FINAL - Leandro Fanelli - Este script contiene lo relacionado al enemigo llamado GHOST, el cual hereda de Enemies atributos y aplica Interfaces.
// Ademas se hace uso de Getters y Setters para las transiciones de comportamientos.

public class Ghost : Enemies, IEnemy
{
    MonoBehaviour patrol, stayPos, attack, chaseCharacter;
    [SerializeField] float viewDistance;
    protected bool ghostAttack, stucked;
    float distance, dot;
    GameObject matObj;
    [SerializeField] ParticleSystem ps;
    [SerializeField] GameObject stunPs, stunStarPs;
    bool canChase;
    public bool stuned;

    public float Distance
    {
        get
        {
            return distance;
        }
    }

    public bool GhostAttack
    {
        get { return ghostAttack; }
        set { ghostAttack = value; }
    }

    // Update is called once per frame
    void Start()
    {
        Behaviors();
    }

    void Update()
    {
        mode = gameState.GetPlaneMode();
        Transitions();
    }

    //Se listan todos los comportamientos del enemigo y se guardan en variables
    public void Behaviors()
    {
        patrol = gameObject.GetComponent<Patrol>();
        stayPos = gameObject.GetComponent<StayPos>();
        attack = gameObject.GetComponent<ghAttack>();
        chaseCharacter = gameObject.GetComponent<ChaseCharacter>();
    }

    public void Stunned()
    {
        nma.SetDestination(transform.position);
        actualTime -= Time.deltaTime;
    }

    //En este caso este enemigo tiene un rango de vision, que los demas enemigos no, por eso las variables unicas se setean aca.
    public void Variables()
    {
        distance = Vector3.Distance(characterPos.position, transform.position);
        Vector3 vectorAPJ = characterPos.position - transform.position;
        vectorAPJ.Normalize();
        dot = Vector3.Dot(transform.forward, vectorAPJ);
    }

    //Contiene las transiciones de que comportamiento tiene que actuar en cierto momento
    public void Transitions()
    {
        if (actualTime > 0)
        {
            stuned = true;
            stunStarPs.SetActive(true);
            stunPs.SetActive(true);
            /*if (gameObject.GetComponentInChildren<Animator>() != null)
            {
                var animator = GetComponentInChildren<Animator>();
                animator.SetBool("Stun", true);
            }*/

            Stunned();
            patrol.enabled = false;
            stayPos.enabled = false;
            attack.enabled = false;
            chaseCharacter.enabled = false;
        }
        else
        {
            stuned = false;
            stunStarPs.SetActive(false);
            stunPs.SetActive(false);
            if (gameObject.GetComponentInChildren<Animator>() != null)
            {
                var animator = GetComponentInChildren<Animator>();
                animator.SetBool("Stun", false);
            }
            Variables();
            stayPos.enabled = false;
            if (!ghostAttack)
            {
                attack.enabled = false;
                CheckPath();
                if (dot > 0.55 && distance < viewDistance && canChase || distance < viewDistance / 3 && canChase)
                {
                    chaseCharacter.enabled = true;
                    patrol.enabled = false;
                }
                else if (distance > viewDistance || !canChase)
                {
                    patrol.enabled = true;
                    chaseCharacter.enabled = false;
                }
            }
            else
            {
                attack.enabled = true;
                patrol.enabled = false;
                chaseCharacter.enabled = false;
            }
        }

        if (matObj == null && stucked)
        {
            ps.Pause();
            nma.speed = 3.5f;
            stucked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ruler" || other.gameObject.tag == "Cube")
        {
            matObj = other.gameObject;
            ps.Play();
            nma.speed = .3f;
            stucked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ruler" || other.gameObject.tag == "Cube")
        {
            ps.Pause();
            nma.speed = 3.5f;
            stucked = false;
        }
    }

    void CheckPath()
    {
        NavMeshPath navMeshStatus = new NavMeshPath();

        if (nma.isActiveAndEnabled)
        {
                nma.CalculatePath(characterPos.position, navMeshStatus);

            if (navMeshStatus.status == NavMeshPathStatus.PathComplete)
            {
                canChase = true;
            }
            else
            {
                canChase = false;
            }
        }
    }


}
