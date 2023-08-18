using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;
    private Vector3[] wallsRestorePos;
    public AudioSource AmbientSound, FallingWalls;
    public DialogManager dialogManager;
    [SerializeField] private TutorialPaperSC tutorialPaperSC;

    private void Start()
    {
        wallsRestorePos = new Vector3[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            wallsRestorePos[i] = walls[i].transform.localPosition;
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i].transform.localPosition.y < -40)
            {
                walls[i].SetActive(false);
            }
        }

        if (tutorialPaperSC.tutorialMat2Finishing)
        {
            DropWalls();
            tutorialPaperSC.tutorialMat2Finishing = false;
        }
    }

    public void EventRestoreWalls()
    {
        RestoreWalls();
    }
    
    public void DropWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        AmbientSound.Play();
        FallingWalls.Play();
    }
    private void RestoreWalls()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].SetActive(true);
            walls[i].GetComponent<Rigidbody>().isKinematic = true;
            walls[i].transform.localPosition = wallsRestorePos[i];
        }
        dialogManager.ShowDialog(DialogKey.FirstCheckpoint);
    }
}
