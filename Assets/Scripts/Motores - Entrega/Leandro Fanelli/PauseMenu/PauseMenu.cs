using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]GameObject menu;
    TutorialPaperSC tutorialPaperSC;
    // Start is called before the first frame update

    private void Awake()
    {
        tutorialPaperSC = GameObject.Find("ToturialPaperIU").GetComponent<TutorialPaperSC>();
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void OnEnable()
    {
        menu.SetActive(true);
    }

    private void Update()
    {
        if (!tutorialPaperSC.anyTutorialOpen && menu.activeInHierarchy == false)
        {
            menu.SetActive(true);
        }
        else if(tutorialPaperSC.anyTutorialOpen)
        {
            menu.SetActive(false);
        }

        if(PauseStateManager.Instance.CurrentPauseState == PauseState.Gameplay)
        {
            menu.SetActive(false);
        }
    }

    void OnDisable()
    {
        menu.SetActive(false);
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Paused;
    }
}
