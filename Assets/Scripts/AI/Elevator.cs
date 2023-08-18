using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject pointA, pointB, paraguas;
    Vector3 initialPointA;
    public bool grabbed;
    public float timeTraveler;
    Vector3 velocity;
    public PlayerCollitionsBody plCol;
    // Start is called before the first frame update
    void Start()
    {
        initialPointA = pointA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(pointA.transform.position, pointB.transform.position);



        if (Input.GetButtonDown("Jump") || distance < 5f)
        {
            GameObject.Find("Char").GetComponent<Rigidbody>().isKinematic = false;
            grabbed = false;
            GameObject.Find("Char").transform.parent = null;
            paraguas.SetActive(false);
            GameObject.Find("CameraHolder").GetComponent<MoveCamera>().dontMoveCamera = false;
        }
    }

    private void FixedUpdate()
    {
        if (grabbed == true)
        {
            GameObject.Find("Char").transform.parent = pointA.transform;
            GameObject.Find("Char").GetComponent<Rigidbody>().isKinematic = true;
            pointA.transform.position = Vector3.SmoothDamp(pointA.transform.position, pointB.transform.position, ref velocity, timeTraveler * Time.deltaTime);
            GameObject.Find("CameraHolder").GetComponent<MoveCamera>().dontMoveCamera = true;
        }
        else
        {
            pointA.transform.position = Vector3.SmoothDamp(pointA.transform.position, initialPointA, ref velocity, timeTraveler * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Char" && plCol.canElevate == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Char" && plCol.canElevate == true)
        {
            if (Input.GetButton("Interact"))
            {
                other.transform.position = gameObject.transform.position;
                paraguas.SetActive(true);
                grabbed = true;
                transform.GetChild(0).gameObject.SetActive(false);


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Char" && plCol.canElevate == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
