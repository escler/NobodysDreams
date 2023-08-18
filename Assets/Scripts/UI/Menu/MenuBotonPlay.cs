using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuBotonPlay : MonoBehaviour
{
    public GameObject controls, arrow, backArrow, closeControls;
    public GameObject[] controlPapers;
    int actualControl;
    public AudioSource click;
    public void ChangeSceneStart(string Cutscene)
    {
        SceneManager.LoadScene(Cutscene);
        click.Play();
    }
    public void ShowControls()
    {
        controls.SetActive(true);
        click.Play();
    }
    public void HideControls()
    {
        if(controls.activeInHierarchy)
        {
            controls.SetActive(false);
            click.Play();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            click.Play();
            controls.SetActive(false);
        }
    }

    public void Continue()
    {
        PauseState currentPauseState = PauseStateManager.Instance.CurrentPauseState;
        PauseState newPauseState = currentPauseState == PauseState.Paused
            ? PauseState.Gameplay
            : PauseState.Paused;

        PauseStateManager.Instance.SetState(newPauseState);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MenuControls()
    {
        actualControl = 0;
        controlPapers[actualControl].SetActive(true);
        arrow.SetActive(true);
        backArrow.SetActive(false);
        actualControl = 0;
        closeControls.SetActive(true);
    }

    public void NextControl()
    {
        controlPapers[actualControl].SetActive(false);
        actualControl++;
        controlPapers[actualControl].SetActive(true);

        if (actualControl == controlPapers.Length - 1)
            arrow.SetActive(false);

        if(actualControl > 0)
        {
            backArrow.SetActive(true);
        }
        else
        {
            backArrow.SetActive(false);
        }

        print(actualControl);
    }

    public void BackControl()
    {
        print(actualControl);
        controlPapers[actualControl].SetActive(false);
        actualControl--;
        controlPapers[actualControl].SetActive(true);

        if (actualControl > 0)
        {
            backArrow.SetActive(true);
        }
        else
        {
            backArrow.SetActive(false);
        }

        if (actualControl == controlPapers.Length - 1)
            arrow.SetActive(false);
        else
            arrow.SetActive(true);
    }

    public void CloseControlMenu()
    {
        controlPapers[actualControl].SetActive(false);
        actualControl = 0;
        arrow.SetActive(false);
        backArrow.SetActive(false);
        closeControls.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
