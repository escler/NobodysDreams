using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlueSquareTab : MonoBehaviour
{
    [SerializeField] public bool correctButton1, correctButton2, correctButton3, correctButton4;
    [SerializeField] public bool incorrectButton1, incorrectButton2, finalPass;
    [SerializeField] private GameObject[] buttonsLightDisable;
    [SerializeField] private GameObject doorCollider, lightOfDoorgreen, lightOfDoorred;
    //declarar un sonido de incorrecto

    public void DoorCollider(bool enableDoor)
    {
        doorCollider.GetComponent<Collider>().enabled = enableDoor;
    }
    void Update()
    {
        if (correctButton1 && correctButton2 && correctButton3 && correctButton4)
        {
            if (incorrectButton1 || incorrectButton2)
            {
                //hacer un sonido de incorrecto
                //
                correctButton1 = false;
                correctButton2 = false;
                correctButton3 = false;
                correctButton4 = false;
                incorrectButton1 = false;
                incorrectButton2 = false;

                for (int i = 0; i < buttonsLightDisable.Length; i++)
                {
                    buttonsLightDisable[i].SetActive(false);
                }
            }
            else
            {
                //hacer un sonido de correcto
                //
                correctButton1 = false;
                correctButton2 = false;
                correctButton3 = false;
                correctButton4 = false;
                lightOfDoorgreen.SetActive(true);
                lightOfDoorred.SetActive(false);

                DoorCollider(true);
            }

        }
    }
}
