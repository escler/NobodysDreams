using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UmbrellaUI : MonoBehaviour
{
    Slider slider;
    Umbrella umbrella;
    [SerializeField] GameObject disable;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        umbrella = GameObject.Find("Char").GetComponentInChildren<Umbrella>();
        slider.maxValue = umbrella.MaxTimeUmbrella;
        slider.value = umbrella.ActualTime;

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, umbrella.ActualTime, 0.1f);

        if (umbrella.UmbrelaDischarge)
        {
            disable.SetActive(true);
        }
        else
        {
            disable.SetActive(false);
        }
    }
}
