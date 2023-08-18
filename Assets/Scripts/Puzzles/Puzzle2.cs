using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    public bool doorEnable1 = false;
    public bool doorEnable2 = false;
    public bool doorEnable3 = false;
    public bool practice1 = false;
    public bool practice2 = false;
    public bool practice3 = false;
    public DialogManager dialogManager;
    public Animator animatorWall;
    public GameObject particle1, particle2, particle3, diana1, diana2, diana3, ligthOff, lightLamp, particle4, particle5, particle6;
    public AudioSource audioSource, primeraDiana;
    public AudioClip openWall;
    bool playSound;

    void Update()
    {
        if (practice1 && practice2 && practice3)//activa primera ddiana
        {
            dialogManager.ShowDialog(DialogKey.FirstDiana);
            ligthOff.SetActive(false);
            diana1.SetActive(true);
            primeraDiana.Play();
            practice3 = false;
        }
        if (doorEnable1 && practice1)//desactiva particula de primera diana y activa 2da diana
        {
            particle4.SetActive(false);

            particle1.SetActive(true);
            diana2.SetActive(true);
            primeraDiana.Play();
            practice1 = false;
            practice2 = true;
        }
        if (doorEnable2 && practice2)//desactiva particula de segunda diana y activa 3da diana
        {
            particle5.SetActive(false);

            particle2.SetActive(true);
            diana3.SetActive(true);
            primeraDiana.Play();
            practice2 = false;
            practice3 = true;
        }
        if (doorEnable3 && practice3)//desactiva particula de tercera diana y activa 3da diana
        {
            particle6.SetActive(false);

            primeraDiana.Play();
            practice3 = false;
        }
        if (doorEnable1 && doorEnable2 && doorEnable3)
        {
            particle3.SetActive(true);
            lightLamp.SetActive(true);

            dialogManager.ShowDialog(DialogKey.Walls);
            doorEnable3 = false;
            animatorWall.SetBool("EnableMove", true);
            if (!playSound)
            {
                audioSource.PlayOneShot(openWall);
                playSound = true;
            }
        }
    }
}
