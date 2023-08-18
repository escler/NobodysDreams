using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeenThinks : MonoBehaviour
{
    private float currRot;
    private bool startRot = false;
    public GameObject partTwoOflevel2, particle, wall, enemy1, enemy2;
    public DialogManager dialogManager;
    bool talk;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball(Clone)" && gameObject.name == "OreoL")
        {
            startRot = true;
            gameObject.GetComponent<KinematicMovementController>().enabled = true;
        }
        else if (collision.gameObject.name == "Ball(Clone)" && gameObject.name == "Pizarron")
        {
            startRot = true;
            gameObject.GetComponent<KinematicMovementController>().enabled = true;
            partTwoOflevel2.SetActive(true);
            particle.SetActive(false);
            wall.GetComponent<Rigidbody>().isKinematic = false;
            wall.GetComponent<Collider>().isTrigger = true;
            enemy1.SetActive(true);
            enemy2.SetActive(true);
        }
    }
    private void Update()
    {
        if (wall.transform.position.y <= -38)
        {
            wall.SetActive(false);
            if (!talk)
            {
                dialogManager.ShowDialog(DialogKey.CarefullGhosts);
                talk = true;
            }
        }
        if (gameObject.name == "OreoL" && startRot)
        {
            currRot = gameObject.transform.rotation.x;
            if (currRot >= 1f)
            {
                gameObject.GetComponent<KinematicMovementController>().enabled = false;
                startRot = false;
            }
        }
        else if (gameObject.name == "Pizarron" && startRot)
        {
            currRot = gameObject.transform.rotation.y;
            if (currRot >= 0.9124448f)
            {
                gameObject.GetComponent<KinematicMovementController>().enabled = false;
                startRot = false;
            }
        }
    }
}
