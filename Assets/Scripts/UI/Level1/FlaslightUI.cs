using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlaslightUI : MonoBehaviour
{
    Slider slider;
    FlashLight flashlight;
    [SerializeField] GameObject disable;
    bool disableBool = false;

    void Start()
    {
        flashlight = GameObject.Find("Char").GetComponentInChildren<FlashLight>();
        slider = GetComponent<Slider>();
        slider.maxValue = flashlight.MaxBattery;
    }

    void Update()
    {
        slider.value = flashlight.ActualBattery;

        if (flashlight.BatteryDead || disableBool == true)
        {
            disable.SetActive(true);
        }
        else
        {
            disable.SetActive(false);
        }
    }

    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        disableBool = planeMode == GameState.PlaneMode.Ghost;
    }
}
