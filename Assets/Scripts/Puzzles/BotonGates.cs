using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonGates : MonoBehaviour
{
    [SerializeField] private MiniBoss miniBoss;
    [SerializeField] public GameObject ball2GO;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball(Clone)" && gameObject.name == "ResetGreen")
        {
            miniBoss.gateGreenOpen = true;
        }
        else if (collision.gameObject.name == "Ball(Clone)" && gameObject.name == "ResetBlue")
        {
            miniBoss.gateBlueOpen = true;
            ball2GO.SetActive(true);
        }
        else if (collision.gameObject.name == "Ball(Clone)" && gameObject.name == "ResetPink")
        {
            miniBoss.gatePinkOpen = true;
            ball2GO.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
