using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    float Timer;
    public Animator reloj;
    TimerController tc;
    void Start()
    {
        tc = GameObject.Find("TimerController").GetComponent<TimerController>();
        Timer = 2f;
        reloj = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloj.enabled == true)
        {
            Timer -= Time.deltaTime;
            if(Timer<= 0)
            {
                reloj.enabled = false;
                Timer = 2f;
            }
        }

        if(tc.EnemiesAttacking > 0)
        {
            reloj.enabled = true;
            Timer = 2f;
        }
    }
}
