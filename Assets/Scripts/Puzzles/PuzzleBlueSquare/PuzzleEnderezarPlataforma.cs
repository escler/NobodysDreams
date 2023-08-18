using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEnderezarPlataforma : MonoBehaviour
{
    public bool limitOfRotationZ, cutRope;//poner en true cuando corte la soga

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
        {
            limitOfRotationZ = true;
        }
    }
    void Update()
    {

        if (cutRope)
        {

            if (limitOfRotationZ)
            {
                gameObject.GetComponent<KinematicMovementController>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<KinematicMovementController>().enabled = true;
            }
        }
    }
}
