using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDown : MonoBehaviour
{
    public bool doorDown1 = true;
    public bool doorDown2 = true;

    void Update()
    {
        if (doorDown1 && doorDown2)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        //if (gameObject.transform.position.y <= -0.068)
        //{
        //    doorDown2 = false;
        //}
    }
}
