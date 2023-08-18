using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionEvents : MonoBehaviour
{
    [SerializeField] private FinalPuzzle finalPuzzle;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Char")
        {
            //esquema 1
            if (gameObject.name == "SlimeFloor1")//slime1
            {
                finalPuzzle.button1enable = true;
            }

            if (gameObject.name == "Medio1")//medio1
            {
                finalPuzzle.backMonster1 = true;
            }

            //esquema 2
            if (gameObject.name == "SlimeFloor2")//slime2
            {
                finalPuzzle.button2enable = true;
            }

            if (gameObject.name == "Medio2")//medio2
            {
                finalPuzzle.backMonster2 = true;
            }

            //esquema 3
            if (gameObject.name == "SlimeFloor3")//slime2 cambiar nombre
            {
                finalPuzzle.button3enable = true;
            }

            if (gameObject.name == "apago la luz")
            {
                finalPuzzle.disableall = true;
            }

            if (gameObject.name == "prenderLuz")
            {
                finalPuzzle.prenderLuz = true;
            }
            if (gameObject.name == "cielo enable")
            {
                finalPuzzle.cielo = true;
            }
        }


        if (collision.gameObject.name == "Ball(Clone)")
        {
            //esquema 1
            if (gameObject.name == "ON (1)")//aparece el monstruo
            {
                finalPuzzle.particle1Off = true;
                finalPuzzle.lightUp1 = true;
            }
            if (gameObject.name == "triger")//para rotar aparece plataformas
            {
                finalPuzzle.triger = true;
            }

            //esquema 2
            if (gameObject.name == "ON (2)")//aparece el monstruo CA,BIAR NOOMBRE AL BOTON
            {
                finalPuzzle.particle2Off = true; 
                finalPuzzle.lightUp2 = true;
            }
            if (gameObject.name == "triger1")//para rotar aparece plataformas
            {
                finalPuzzle.triger1 = true;
            }

            //esquema 3
            if (gameObject.name == "ON (3)")//aparece el monstruo CA,BIAR NOOMBRE AL BOTON
            {
                finalPuzzle.particle3Off = true;
                finalPuzzle.lightUp3 = true;
            }
            if (gameObject.name == "triger2")//para rotar aparece plataformas
            {
                finalPuzzle.triger2 = true;
            }

            //luz
            if (gameObject.name == "Ultima luz")//
            {
                finalPuzzle.backMonster3 = true;
            }

        }

    }

}
