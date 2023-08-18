using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacasDePresion : MonoBehaviour
{
    [SerializeField] private GameObject disableParticle, enableParticle;
    [SerializeField] private PuzzlePlacasdePresion placasdePresion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Pesita")
        {
            placasdePresion.presureCount++;
            disableParticle.SetActive(false);
            enableParticle.SetActive(true);
        }
    }

}
