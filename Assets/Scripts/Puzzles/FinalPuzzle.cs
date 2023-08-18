using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzle : MonoBehaviour
{
    [SerializeField] GameObject button1GO, particle1GO, lightUp1GO, lightRot1GO, medio1GO, esqueme2GO, 
                                button2GO, particle2GO, lightUp2GO, lightRot2GO, medio2GO, esqueme3GO,
                                button3GO, particle3GO, lightUp3GO, lightRot3GO, medio3GO, balloonEsqueme,
                                bossGO, lightTorchGO, prenderLuzGO, cieloGO, charGO;
    [SerializeField] public bool button1enable, particle1Off, lightUp1, triger, backMonster1,
                                 button2enable, particle2Off, lightUp2, triger1, backMonster2,
                                 button3enable, particle3Off, lightUp3, triger2, backMonster3, disableall, prenderLuz, cielo;
    [SerializeField] public Transform quintaPosicion, cuartaPosicion, terceraPosicion, segundaPosicion, posicionInicial;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public BossBehaviours bossBehaviours;
    private float posY;

    private void Update()
    {
        if (button1enable)//ANDA
        {
            button1GO.SetActive(true);
            button1enable = false;
            bossGO.SetActive(true);

            bossBehaviours.ShowMonster = true;//APARECE EL MONSTRUO
            //audioSource.Play();//grita
            
        }
        if (particle1Off)
        {
            particle1GO.SetActive(false);//apaga
            particle1Off = false;
            balloonEsqueme.SetActive(false);
        }
        if (lightUp1)
        {
            bossBehaviours.MoveBoss(segundaPosicion);//mueve EL MONSTRUO
            lightUp1 = false;
            lightUp1GO.SetActive(true);
        }
        if (triger)
        {
            lightUp1GO.SetActive(false);
            lightRot1GO.SetActive(true);
            esqueme2GO.SetActive(true);
            medio1GO.SetActive(true);
            bossBehaviours.MoveBoss(terceraPosicion);//mueve EL MONSTRUO

            triger = false;
        }
        if (backMonster1)
        {
            bossBehaviours.ShowMonster = false;//desAPARECE EL MONSTRUO
            //audioSource.Play();//grita
            backMonster1 = false;
        }

        //esquema 2
        if (button2enable)
        {
            button2GO.SetActive(true);
            button2enable = false;

            bossBehaviours.ShowMonster = true;//APARECE EL MONSTRUO
            //audioSource.Play();//grita
        }
        if (particle2Off)
        {
            particle2GO.SetActive(false);//apaga
            particle2Off = false;
        }
        if (lightUp2)
        {
            lightUp2GO.SetActive(true);
            lightUp2 = false;
        }
        if (triger1)
        {
            lightUp2GO.SetActive(false);
            lightRot2GO.SetActive(true);
            esqueme3GO.SetActive(true);
            medio2GO.SetActive(true);
            bossBehaviours.MoveBoss(cuartaPosicion);//mueve EL MONSTRUO

            triger1 = false;
        }
        if (backMonster2)
        {
            bossBehaviours.ShowMonster = false;//desAPARECE EL MONSTRUO
            //audioSource.Play();//grita
            backMonster2 = false;
        }

        //esquema 3
        if (button3enable)
        {
            button3GO.SetActive(true);
            button3enable = false;

            bossBehaviours.ShowMonster = true;//APARECE EL MONSTRUO
            //audioSource.Play();//grita
        }
        if (particle3Off)
        {
            particle3GO.SetActive(false);//apaga
            particle3Off = false;
        }
        if (lightUp3)
        {
            lightUp3GO.SetActive(true);
            lightUp3 = false;
        }
        if (triger2)
        {
            lightUp3GO.SetActive(false);
            lightRot3GO.SetActive(true);
            medio3GO.SetActive(true);
            bossBehaviours.MoveBoss(quintaPosicion);//mueve EL MONSTRUO

            triger2 = false;
        }

        //piso globo
        if (backMonster3)
        {
            bossBehaviours.MoveBoss(posicionInicial);//mueve EL MONSTRUO
            bossBehaviours.ShowMonster = false;//desAPARECE EL MONSTRUO
            bossGO.SetActive(false);
            balloonEsqueme.SetActive(true);
            lightTorchGO.SetActive(true);

            //audioSource.Play();//grita
            backMonster3 = false;
        }

        if (disableall)
        {
            lightTorchGO.SetActive(true);
            disableall = false;
        }

        if (prenderLuz)
        {
            prenderLuzGO.SetActive(true);
            prenderLuz = false;
        }

        if (cielo)
        {
            cieloGO.SetActive(true);
            cielo = false;
        }

    }
}
