using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvAccordToPlane : MonoBehaviour
{
    private GameState gameState;
    public GameObject iconGhostOn, iconGhostOff, iconDreamOn, iconDreamOff, canvasBallCount, disableFlashlight, pillsGO, umbrellaPick, umbrellaUI;
    public Umbrella umbrella;
    public PlayerSC playerSC;
    private GameState.PlaneMode lastAppliedPlaneMode;
    public AudioSource SwitchMode;

    void Awake()
    {
        gameState = FindObjectOfType<GameState>();
        lastAppliedPlaneMode = gameState.GetPlaneMode();
    }
    void Update()
    {
        GameState.PlaneMode currAppliedPlaneMode = gameState.GetPlaneMode();
        if (currAppliedPlaneMode == lastAppliedPlaneMode)
            return;

        lastAppliedPlaneMode = currAppliedPlaneMode;

        switch (currAppliedPlaneMode)
        {
            case GameState.PlaneMode.Dream:
                iconGhostOn.SetActive(false);
                iconGhostOff.SetActive(true);
                iconDreamOn.SetActive(true);
                iconDreamOff.SetActive(true);
                pillsGO.SetActive(false);
                umbrella.disableInPlaneDream = true;
                umbrellaUI.SetActive(false);
                if (umbrellaPick != null)
                {
                    umbrellaPick.SetActive(false);
                }

                SwitchMode.Play();
                break;
            case GameState.PlaneMode.Ghost:
                iconGhostOn.SetActive(true);
                iconGhostOff.SetActive(false);
                iconDreamOn.SetActive(false);
                iconDreamOff.SetActive(true);
                pillsGO.SetActive(true);
                umbrella.disableInPlaneDream = false;
                umbrellaUI.SetActive(true);
                if (umbrellaPick != null)
                {
                    umbrellaPick.SetActive(true);
                }

                SwitchMode.Play();
                break;
            case GameState.PlaneMode.Demon:
                break;
        }
    }
}
