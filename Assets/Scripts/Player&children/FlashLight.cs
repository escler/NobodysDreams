using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    Light flashLight;
    public AudioSource flashLightOn, flashLightOff;
    public GameObject flashLightModelGO;
    public float MinTime;
    public float MaxTime;
    public float Timer;
    public float posibility;
    bool flicker;
    public bool hasFlashlight = false;
    [SerializeField] float maxBattery, dischargeAmount, baseDischargeAmount, rechargeAmount, rechargeAmountDeadBattery;
    float actualBattery, flickerDuration, switchLight;
    bool usingFL, canUseFL, batteryDead;
    bool canUseFlashlight = false;//TP2 - Caamaño Romina - pregunta si puede usar la linterna segun el plano


    public bool FlashLightEnabled
    {
        get
        {
            return flashLight.enabled;
        }
    }

    public bool Flicker(bool flickerState)
    {
        return flicker = flickerState;
    }

    public float ActualBattery
    {
        get
        {
            return actualBattery;
        }
        set
        {
            actualBattery = value;
        }
    }

    public float MaxBattery
    {
        get
        {
            return maxBattery;
        }
    }

    public bool BatteryDead
    {
        get
        {
            return batteryDead;
        }
    }

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Start()
    {
        flashLight = GetComponent<Light>();
        Timer = Random.Range(MinTime, MaxTime);
        actualBattery = maxBattery;
    }

    void Update()
    {
        if(!batteryDead && actualBattery > 0)
        {
            canUseFL = true;
        }
        else
        {
            canUseFL = false;
        }

        if (hasFlashlight)
        {
            canUseFlashlight = hasFlashlight;
            hasFlashlight = false;
        }
        flashLightModelGO.GetComponent<MeshRenderer>().enabled = canUseFlashlight;

        if (!canUseFlashlight)
        {
            flashLight.enabled = false;
        }
        else if (Input.GetButtonDown("LeftClick") && canUseFL)
        {
            if (flashLight.enabled)
            {
                flashLight.enabled = false;
                flashLightOff.Play();
                usingFL = false;
            }
            else
            {
                usingFL = true;
                flashLight.enabled = true;
                flashLightOn.Play();
                actualBattery -= baseDischargeAmount;
            }
        }

        if (usingFL)
        {
            actualBattery -= dischargeAmount * Time.deltaTime;
        }

        if (actualBattery <= 0)
        {
            batteryDead = true;
        }
        else if (actualBattery > 0 && flashLight.enabled == false && actualBattery < maxBattery)
        {
            actualBattery += rechargeAmount * Time.deltaTime;
        }

        if (actualBattery > maxBattery)
        {
            actualBattery = maxBattery;
        }

        if (batteryDead)
        {
            usingFL = false;
            flashLight.enabled = false;
            actualBattery += rechargeAmountDeadBattery * Time.deltaTime;
            if (actualBattery >= maxBattery)
            {
                actualBattery = maxBattery;
                batteryDead = false;
            }
        }
    }

    //TP2 - Caamaño Romina - Segun el plano determinado en el Game State se habilita el uso de la linterna
    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        canUseFlashlight = planeMode == GameState.PlaneMode.Dream;
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
