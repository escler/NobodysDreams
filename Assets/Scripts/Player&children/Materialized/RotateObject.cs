using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float speedRotation;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject finalPosObject, actualObject, parent;
    [SerializeField] bool cube;
    [SerializeField] Material blue, red;
    MaterializeObjects matObjs;
    Vector3 finalPos;
    bool ray, destroyFinalPosObject;
    RaycastHit hit;
    MaterializeObjects mo;
    Rigidbody rb;

    public void FinalPosObject()
    {
        //GetComponentInChildren<BoxCollider>().enabled = true;
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        }

        GetComponent<FollowObject>().enabled = true;
        Destroy(actualObject);
        this.enabled = false;
    }

    public void CancelObject()
    {
        Destroy(actualObject);
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        matObjs = GameObject.Find("Char").GetComponent<MaterializeObjects>();

        if(gameObject.tag == "Cube")
        {
            actualObject = Instantiate(finalPosObject, transform.position,transform.rotation);
        }
        else
        {
            actualObject = Instantiate(finalPosObject, parent.transform.position,transform.rotation);
        }

        destroyFinalPosObject = false;
        mo = GameObject.Find("Char").GetComponent<MaterializeObjects>();
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            rb = GetComponentInChildren<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (matObjs.CanMat == false && actualObject.GetComponent<Renderer>().material != red)
        {
            actualObject.GetComponent<Renderer>().material = red;
        }
        else if(matObjs.CanMat == true && actualObject.GetComponent<Renderer>().material != blue)
        {
            actualObject.GetComponent<Renderer>().material = blue;
        }

        if (destroyFinalPosObject && actualObject != null)
        {
            Destroy(actualObject);
        }

        transform.position = mo.PosObject;
        /*Collider[] col = Physics.OverlapBox(gameObject.GetComponent<Collider>().bounds.center, transform.localScale, transform.rotation, layerMask);
        Collider[] orderCol;
        Vector3 closestPoint;

        if (col.Any())
        {
            orderCol = col.OrderBy(x => Vector3.Distance(x.transform.position, mo.PosObject)).ToArray();
            closestPoint = orderCol[0].ClosestPoint(transform.position);
        }
        else if(Physics.Raycast(transform.position,-Vector3.up, out hit, 100f,layerMask))
        {
            closestPoint = hit.point;
        }
        else
        {
            closestPoint = mo.transform.position;
        }*/

        float rotation = Input.GetAxis("RotateObject") * speedRotation;
        gameObject.transform.Rotate(0, rotation * Time.deltaTime, 0);

        if (cube)
        {
            actualObject.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            //actualObject.transform.position = new Vector3(transform.position.x, closestPoint.y, transform.position.z);
        }
        else
        {
            actualObject.transform.position = parent.transform.position;
            //actualObject.transform.position = new Vector3(transform.position.x, closestPoint.y, transform.position.z);
        }



        actualObject.transform.rotation = transform.rotation;

        if(parent != null)
        {
            finalPos = transform.position;
        }
        else
        {
            finalPos = actualObject.transform.position;
        }

    }

    public Vector3 FinalPos
    {
        get
        {
            return finalPos;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 0)
        {
            if (collision.gameObject.tag == "MobilePlatforms")
            {
                transform.parent = collision.transform;
            }
        }
        
    }

    public void Freeze()
    {

    }



    /*IEnumerator WaitPhysics()
    {
        yield return new WaitForSeconds(0.5f);

        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;
    }*/


}
