using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallColumn : MonoBehaviour
{
    public GameObject bookWheelSC;
    public DoorDown doorDown;
    public AudioSource audioSource;
    //private Vector3 position1;

    private void Start()
    {
        //position1 = doorGO.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball(Clone)")
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            bookWheelSC.GetComponent<Wheel>().enabled = true;
            foreach (Collider collider in bookWheelSC.GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            audioSource.Play();
            if (gameObject.name == "Columnacaida - 1")
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Collider>().enabled = false;
                doorDown.doorDown1 = true;
            }
            if (gameObject.name == "Columnacaida - 2")
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Collider>().enabled = false;
                doorDown.doorDown2 = true;
            }
        }
    }
    private void Update()
    {
        if (bookWheelSC.transform.rotation.z >= 0.2764)
        {
            audioSource.Stop();
            bookWheelSC.GetComponent<Wheel>().enabled = false;
        }
    }
}
