using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonGates : MonoBehaviour
{
    [SerializeField] private MiniBoss miniBoss;
    [SerializeField] public GameObject ball2GO;
    private ParticleSystem aura;

    private void Awake()
    {
        aura = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 17 && gameObject.name == "ResetGreen")
        {
            miniBoss.gateGreenOpen = true;
            aura.gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == 17 && gameObject.name == "ResetBlue")
        {
            miniBoss.gateBlueOpen = true;
            ball2GO.SetActive(true);
        }
        else if (collision.gameObject.layer == 17 && gameObject.name == "ResetPink")
        {
            miniBoss.gatePinkOpen = true;
            ball2GO.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
