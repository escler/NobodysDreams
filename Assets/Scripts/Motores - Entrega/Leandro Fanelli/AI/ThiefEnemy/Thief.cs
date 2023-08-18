using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// FINAL - Leandro Fanelli - Este script contiene lo relacionado al enemigo llamado THIEF, el cual hereda de Enemies atributos y aplica Interfaces.
// Ademas se hace uso de Getters y Setters para las transiciones de comportamientos.
public class Thief : Enemies, IEnemy
{
    MonoBehaviour patrol, stayPos, moveToMatObject, escape, stoleItem;
    Transform objectPos;
    MaterializeObjects matObjs;
    float distance, timeToSteal;
    bool objectGrabbed, stealObject, objectToSteal;
    public bool stuned, cantSteal;
    AudioSource audioSource;
    [SerializeField]AudioClip laugh;
    [SerializeField] GameObject rulerInEnemy, cubeInEnemy, stunPS, stunStarPS, thunderPs;
    // Start is called before the first frame update

    public float Distance
    {
        get
        {
            return distance;
        }
    }

    public bool StealObject
    {
        get
        {
            return stealObject;
        }

        set
        {
            stealObject = value;
        }
    }

    public bool ObjectGrabbed
    {
        get
        {
            return objectGrabbed;
        }

        set
        {
            objectGrabbed = value;
        }
    }

    public bool ObjectToSteal
    {
        get
        {
            return objectToSteal;
        }

        set
        {
            objectToSteal = value;
        }
    }

    public Transform ObjectPos
    {
        get
        {
            return objectPos;
        }
    }

    void Start()
    {
        Behaviors();
        audioSource = GetComponent<AudioSource>();
        matObjs = GameObject.Find("Char").GetComponent<MaterializeObjects>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(objectGrabbed);
        //print(objectToSteal);
        mode = gameState.GetPlaneMode();
        if(timeToSteal > 0)
        {
            timeToSteal -= Time.deltaTime;
        }

        if(actualTime > 0)
        {
            stunPS.SetActive(true);
            stunStarPS.SetActive(true);
        }
        else
        {
            stunPS.SetActive(false);
            stunStarPS.SetActive(false);
        }


        if(timeToSteal > 0 && actualTime <= 0)
        {
            thunderPs.SetActive(true);
            cantSteal = true;
        }
        else
        {
            thunderPs.SetActive(false);
            cantSteal = false;
        }

        Transitions();
    }

    //Se listan todos los comportamientos del enemigo y se guardan en variables
    public void Behaviors()
    {
        patrol = gameObject.GetComponent<Patrol>();
        stayPos = gameObject.GetComponent<StayPos>();
        moveToMatObject = gameObject.GetComponent<MoveToMatObject>();
        escape = gameObject.GetComponent<Escape>();
        stoleItem = gameObject.GetComponent<StoleItem>();
    }

    public void Stunned()
    {
        matObjs.CanMat = true;
        timeToSteal = 7f;
        nma.SetDestination(transform.position);
        actualTime -= Time.deltaTime;
        rulerInEnemy.SetActive(false);
        cubeInEnemy.SetActive(false);
        objectGrabbed = false;

    }

    //Este enemigo solo necesita calcular la distancia del enemigo
    public void Variables()
    {
        distance = Vector3.Distance(characterPos.position, transform.position);
    }

    //Contiene las transiciones de que comportamiento tiene que actuar en cierto momento
    public void Transitions()
    {
        if (actualTime > 0)
        {
            stuned = true;
            Stunned();
            patrol.enabled = false;
            stayPos.enabled = false;
            moveToMatObject.enabled = false;
            escape.enabled = false;
            stoleItem.enabled = false;
        }
        else
        {
            stuned = false;
            Variables();
            stayPos.enabled = false;
            if (!objectGrabbed)
            {
                if (objectToSteal && timeToSteal <= 0)
                {
                    if (stealObject)
                    {
                        stoleItem.enabled = true;
                        moveToMatObject.enabled = false;
                        patrol.enabled = false;
                        audioSource.PlayOneShot(laugh);
                    }
                    else
                    {
                        moveToMatObject.enabled = true;
                        patrol.enabled = false;
                        stoleItem.enabled = false;
                    }
                }
                else
                {
                    patrol.enabled = true;
                    moveToMatObject.enabled = false;
                    stoleItem.enabled = false;
                    escape.enabled = false;
                }

            }
            else
            {
                escape.enabled = true;
            }
        }
    }

    //Al colocarse un objeto robable, calcula si hay un camino posible dentro del NavMesh, en caso que si se activa el comportamiento para ir a robarlo, pasandole la posicion del objeto
    public void CheckPosObject(Transform pos)
    {
        NavMeshPath navMeshStatus = new NavMeshPath();
        nma.CalculatePath(pos.position, navMeshStatus);

        if (navMeshStatus.status == NavMeshPathStatus.PathComplete)
        {
            objectToSteal = true;
            objectPos = pos;
        }
    }
}
