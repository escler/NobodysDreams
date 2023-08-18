using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsTabPuzzle : MonoBehaviour
{
    [SerializeField] private PuzzleBlueSquareTab puzzleBlueSquareTab;
    [SerializeField] public GameObject lightCorrButton1, lightCorrButton2, lightCorrButton3, lightCorrButton4;
    [SerializeField] public GameObject lightInButton1, lightInButton2;
    //declarar un audio clip

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball(Clone)" && puzzleBlueSquareTab.finalPass)
        {
            //reproducir un sonido de click

            if (gameObject.name == "Correct1")
            {
                puzzleBlueSquareTab.correctButton1 = true;
                lightCorrButton1.SetActive(true);
            }
            else if (gameObject.name == "Correct2")
            {
                puzzleBlueSquareTab.correctButton2 = true;
                lightCorrButton2.SetActive(true);
            }
            else if (gameObject.name == "Correct3")
            {
                puzzleBlueSquareTab.correctButton3 = true;
                lightCorrButton3.SetActive(true);
            }
            else if (gameObject.name == "Correct4")
            {
                puzzleBlueSquareTab.correctButton4 = true;
                lightCorrButton4.SetActive(true);
            }

            if (gameObject.name == "Incorrect1")
            {
                puzzleBlueSquareTab.incorrectButton1 = true;
                lightInButton1.SetActive(true);
            }
            else if (gameObject.name == "Incorrect2")
            {
                puzzleBlueSquareTab.incorrectButton2 = true;
                lightInButton2.SetActive(true);
            }
        }
    }
}
