using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    FlashLight fl;
    Light flLight;
    bool flashlightOn;
    bool stunnedAvaible;
    [SerializeField] float stunDuration, intensityModifier, startIntensity, startAngle, angleModifier, timeForStun, initialTimeForStun;
    [SerializeField] Color standardColor, stunColor;

    private void Start()
    {
        flLight = GetComponentInParent<Light>();
        startIntensity = flLight.intensity;
        startAngle = flLight.spotAngle;
        stunnedAvaible = true;
        initialTimeForStun = timeForStun;
    }

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Update()
    {
        fl = GetComponentInParent<FlashLight>();
        flashlightOn = fl.FlashLightEnabled;

        if (fl.BatteryDead)
        {
            flLight.intensity = startIntensity;
            flLight.spotAngle = startAngle;
            timeForStun = initialTimeForStun;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 && flashlightOn == true)
        {
            if (timeForStun > 0)
            {
                timeForStun -= Time.deltaTime;
                flLight.intensity += intensityModifier * Time.deltaTime;
                flLight.spotAngle += angleModifier * Time.deltaTime;
                fl.gameObject.GetComponent<Light>().color = stunColor;
            }
            else
            {

                other.gameObject.GetComponent<Enemies>().ReceiveStun(stunDuration);
                stunnedAvaible = false;

                GetComponentInParent<FlashLight>().ActualBattery = 0;
            }
            /*if (other.gameObject.GetComponent<Patrol>().enabled) other.gameObject.GetComponent<Patrol>().Stunned(stunDuration);
            if (other.gameObject.GetComponent<ChaseCharacter>().enabled) other.gameObject.GetComponent<ChaseCharacter>().Stunned(stunDuration);
            if (other.gameObject.GetComponent<ChaseCharacter>().enabled) other.gameObject.GetComponent<ChaseCharacter>().Stunned(stunDuration);
            if (other.gameObject.GetComponent<ChaseCharacter>().enabled) other.gameObject.GetComponent<ChaseCharacter>().Stunned(stunDuration);*/


        }
    }

    private void OnTriggerExit(Collider other)
    {
        fl.gameObject.GetComponent<Light>().color = standardColor;
        flLight.intensity = startIntensity;
        flLight.spotAngle = startAngle;
        timeForStun = initialTimeForStun;
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
