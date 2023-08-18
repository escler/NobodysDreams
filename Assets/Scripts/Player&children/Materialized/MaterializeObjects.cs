using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterializeObjects : MonoBehaviour
{
    [SerializeField] GameObject ruler,cube, camPos, particleCube, particleRuler, newParticle;
    [SerializeField] List<GameObject> rulersActive, cubesActive;
    GameObject actualObject, lastObjectCreated, newObject;
    [SerializeField] LayerMask layerMask;
    Umbrella umbrella;
    public bool materializanding = false;
    float timer;
    Vector3 pos;

    public AudioSource fallObj, spawnObj, spawnPosition, cantMatSound, canMatAgainSoundS;
    bool placingObject, canMat, canMatAgaingSound;
    int objectCreated;

    public bool CanMat
    {
        get { return canMat; }
        set { canMat = value; }
    }

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
        canMatAgaingSound = true;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Start()
    {
        timer = 15f;
        canMat = true;
        objectCreated = 0;
        lastObjectCreated = ruler;
        umbrella = GetComponentInChildren<Umbrella>();
    }
    void Update()
    {
        RaycastHit hit;

        bool ray = Physics.Raycast(camPos.transform.position, camPos.transform.forward, out hit, 3f, layerMask);

        if (ray)
        {
            pos = hit.point;
        }
        else
        {
            pos = camPos.transform.position + camPos.transform.forward * 3f;
        }

        if (Input.GetButtonDown("RightClick") && umbrella.UmbrellaActivate == false) 
        {
            materializanding = true;
            if (!placingObject) // Spawnea
            {
                PrevSpawn();
            }
            else
            {
                PlaceObject();
                materializanding = false;
            }
        }

        if (placingObject)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                SwitchItem();
            }
            else if (scroll < 0f)
            {
                SwitchItem();
            }
            fallObj.Play();
        }

        if(Input.GetButtonDown("CancelMat") && placingObject) //Cancela
        {
            CancelObject();
            materializanding = false;
        }
        else if (Input.GetButtonDown("CancelMat"))
        {
            materializanding = false;
        }

        if (!canMat)
        {
            timer -= Time.deltaTime;
            canMatAgaingSound = false;

            if (timer < 0)
            {
                canMat = true;
                timer = 15f;
                Thief thief = GameObject.Find("Thief").GetComponent<Thief>();
                thief.ObjectGrabbed = false;
                if (thief.transform.GetChild(4).gameObject.activeInHierarchy)
                {
                    thief.transform.GetChild(4).gameObject.SetActive(false);
                }
                if (thief.transform.GetChild(5).gameObject.activeInHierarchy)
                {
                    thief.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (!canMatAgaingSound)
            {
                canMatAgainSoundS.Play();
                canMatAgaingSound = true;
            }
        }
        
    }

    public Vector3 PosObject
    {
        get
        {
            return pos;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawRay(camPos.transform.position, camPos.transform.forward.normalized * 6f);
    }*/

    void PrevSpawn()
    {
        spawnPosition.Play();
        newObject = Instantiate(lastObjectCreated, pos, transform.rotation);
        placingObject = true;
        actualObject = newObject;
        actualObject.transform.parent = gameObject.transform;
        actualObject.transform.eulerAngles = new Vector3(actualObject.transform.eulerAngles.x, actualObject.transform.eulerAngles.y + 90, actualObject.transform.eulerAngles.z);
    }

    void PlaceObject()
    {
        if (canMat)
        {
            if (actualObject.tag == "Ruler")
            {
                if (rulersActive.Count == 2)
                {
                    if (rulersActive[0] != null)
                    {
                        newParticle = Instantiate(particleRuler, rulersActive[0].transform.GetChild(3).transform.position, transform.rotation);
                        newParticle.transform.eulerAngles = new Vector3(rulersActive[0].transform.eulerAngles.x, rulersActive[0].transform.eulerAngles.y + 180f, rulersActive[0].transform.eulerAngles.z);
                    }
                    Destroy(rulersActive[0]);
                    rulersActive[0] = rulersActive[1];
                    rulersActive[1] = actualObject;
                }
                else
                {
                    rulersActive.Add(actualObject);
                    objectCreated++;
                }
                lastObjectCreated = ruler;
            }
            else //Posiciona el item
            {
                if (cubesActive.Count == 2)
                {
                    if (cubesActive[0] != null)
                        Instantiate(particleCube, new Vector3(cubesActive[0].transform.position.x, cubesActive[0].transform.position.y - 0.5f, cubesActive[0].transform.position.z),cubesActive[0].transform.rotation);
                    Destroy(cubesActive[0]);
                    cubesActive[0] = cubesActive[1];
                    cubesActive[1] = actualObject;
                }
                else
                {
                    cubesActive.Add(actualObject);
                    objectCreated++;
                }
                lastObjectCreated = cube;
            }

            RotateObject ro;
            ro = actualObject.GetComponent<RotateObject>();

            actualObject.transform.parent = null;
            if (actualObject.GetComponent<Collider>() != null)
            {
                actualObject.GetComponent<Collider>().isTrigger = false;
            }
            else
            {
                actualObject.GetComponentInChildren<Collider>().isTrigger = false;
            }
            actualObject.transform.position = ro.FinalPos;
            actualObject.GetComponent<RotateObject>().FinalPosObject();
            //actualObject.GetComponent<RotateObject>().Spawn();
            placingObject = false;
        }
        else
        {
            cantMatSound.Play();
        }
    }

    void SwitchItem()
    {
        if (actualObject.tag == "Ruler")
        {
            spawnObj.Play();
            actualObject.GetComponent<RotateObject>().CancelObject();
            Destroy(newObject);
            newObject = Instantiate(cube, pos, transform.rotation);
            actualObject = newObject;
            actualObject.transform.parent = gameObject.transform;
        }
        else
        {
            spawnObj.Play();
            actualObject.GetComponent<RotateObject>().CancelObject();
            Destroy(newObject);
            newObject = Instantiate(ruler, pos, transform.rotation);
            actualObject = newObject;
            actualObject.transform.parent = gameObject.transform;
            actualObject.transform.eulerAngles = new Vector3(actualObject.transform.eulerAngles.x, actualObject.transform.eulerAngles.y + 90, actualObject.transform.eulerAngles.z);
        }
    }

    void CancelObject()
    {
        actualObject.GetComponent<RotateObject>().CancelObject();
        placingObject = false;
        materializanding = false;
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}