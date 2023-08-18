using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FINAL - Leandro Fanelli - Se inicializa el estado en Gameplay, cuando se presiona Escape, se consulta el estado actual de la pausa.
// En caso de que sea Pausa lo cambia a Gameplay, y viceversa. Haciendo que se activen y desactiven los scripts subscritos dependiendo su enum.
public class PauseController : MonoBehaviour
{
    bool paused;
    TutorialPaperSC tutorialPaperSC;

    // Start is called before the first frame update
    void Start()
    {
        PauseStateManager.Instance.CurrentPauseState = PauseState.Gameplay;
        tutorialPaperSC = GameObject.Find("ToturialPaperIU").GetComponent<TutorialPaperSC>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(PauseStateManager.Instance.CurrentPauseState);
        if (Input.GetButtonDown("Escape") && !tutorialPaperSC.anyTutorialOpen)
        {
            PauseState currentPauseState = PauseStateManager.Instance.CurrentPauseState;
            PauseState newPauseState = currentPauseState == PauseState.Paused
                ? PauseState.Gameplay
                : PauseState.Paused;

            PauseStateManager.Instance.SetState(newPauseState);
        }


        
    }
}
