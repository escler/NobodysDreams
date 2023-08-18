using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlacasdePresion : MonoBehaviour
{
    [SerializeField] private GameObject platformKinematicEnable;
    public int presureCount = 0;

    void Update()
    {
        if (presureCount == 4)
        {
            platformKinematicEnable.GetComponent<KinematicMovementController>().enabled = true;
        }
    }
}
