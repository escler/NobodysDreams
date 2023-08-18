using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPaperSC : MonoBehaviour
{
    public bool anyTutorialOpen = false;
    public bool showTuturialFlash = false;
    public bool showTutorialGlasses = false;
    public bool showTutorialBall = false;
    public bool showTutorialMat2 = false;
    public bool tutorialMat2Finishing = false;
    public bool showTutorialMat1 = false;
    public bool showGuideTutorialPlane = false;
    public bool showTutorialBeret;
    public bool showTutorialUmbrella;
    public bool showTutorialTeeth;

    private bool nextTutorial = false;
    private float timeDelay = 0.5f;
    public float fillAmount = 1f;
    public DialogManager dialogManager;
    public Image tutorialFlashligth, tutorialGlasses, tutorialBall, tutorialMat2, tutorialMat1, tutorialBeret, tutorialUmbrella, tutorialTeeth;



    public AudioSource closeTutorial;

    void Update()
    {
        //print(nextTutorial);
        if(Input.GetButtonDown("CancelMat") && !showTutorialMat1)
        {
            if (anyTutorialOpen)
            {
                PauseState currentPauseState = PauseStateManager.Instance.CurrentPauseState;
                PauseState newPauseState = currentPauseState == PauseState.Paused
                    ? PauseState.Gameplay
                    : PauseState.Paused;

                PauseStateManager.Instance.SetState(newPauseState);
            }
        }

        TutorialFlashligth(showTuturialFlash);
        TutorialMaterialized1(showTutorialMat1);
        TutorialGlasses(showTutorialGlasses);
        TutorialBall(showTutorialBall);
        TutorialMaterialized2(showTutorialMat2);
        TutorialBeret(showTutorialBeret);
        TutorialUmbrella(showTutorialUmbrella);
        TutorialTeeth(showTutorialTeeth);

        if (Input.GetButtonDown("CancelMat") && showTuturialFlash == true)
        {
            closeTutorial.Play();
            showTuturialFlash = false;
            anyTutorialOpen = false;
            dialogManager.ShowDialog(DialogKey.Flashlight);
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialGlasses == true)
        {
            dialogManager.ShowDialog(DialogKey.Pills);
            closeTutorial.Play();
            showTutorialGlasses = false;
            anyTutorialOpen = false;
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialBall == true)
        {
            dialogManager.ShowDialog(DialogKey.Bucket);
            closeTutorial.Play();
            showTutorialBall = false;
            anyTutorialOpen = false;
        }
        else if (Input.GetButtonDown("CancelMat") && showGuideTutorialPlane == true)// falta guia de boina
        {
            closeTutorial.Play();
            showGuideTutorialPlane = false;
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialMat1 == true)
        {
            closeTutorial.Play();
            showTutorialMat1 = false;
            nextTutorial = true;
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialMat2 == true)
        {
            closeTutorial.Play();
            showTutorialMat2 = false;
            tutorialMat2Finishing = true;
            anyTutorialOpen = false;
            dialogManager.ShowDialog(DialogKey.RulerCube);
        }
        else if(Input.GetButtonDown("CancelMat") && showTutorialBeret == true)
        {
            closeTutorial.Play();
            showTutorialBeret = false;
            anyTutorialOpen = false;
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialUmbrella == true)
        {
            closeTutorial.Play();
            showTutorialUmbrella = false;
            anyTutorialOpen = false;
        }
        else if (Input.GetButtonDown("CancelMat") && showTutorialTeeth == true)
        {
            closeTutorial.Play();
            showTutorialTeeth = false;
            anyTutorialOpen = false;
        }

        if (nextTutorial)
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay <= 0f)
            {
                showTutorialMat2 = true;
                nextTutorial = false;
                timeDelay = 2f;
            }
        }

    }
    public void TutorialFlashligth(bool show)
    {
        if (show)
        {
            tutorialFlashligth.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialFlashligth.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialGlasses(bool show)
    {
        if (show)
        {
            tutorialGlasses.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialGlasses.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialBall(bool show)
    {
        if (show)
        {
            tutorialBall.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialBall.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialTeeth(bool show)
    {
        if (show)
        {
            tutorialTeeth.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialTeeth.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialMaterialized1(bool show)
    {
        if (show)
        {
            tutorialMat1.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialMat1.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialMaterialized2(bool show)
    {
        if (show)
        {
            tutorialMat2.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialMat2.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialBeret(bool show)
    {
        if (show)
        {
            tutorialBeret.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialBeret.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
    public void TutorialUmbrella(bool show)
    {
        if (show)
        {
            tutorialUmbrella.fillAmount += fillAmount * Time.deltaTime;
        }
        else
        {
            tutorialUmbrella.fillAmount -= fillAmount * Time.deltaTime;
        }
    }
}
