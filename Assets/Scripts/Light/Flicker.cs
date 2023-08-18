using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            other.gameObject.GetComponentInChildren<FlashLight>().Flicker(true);
        }
    }
}
