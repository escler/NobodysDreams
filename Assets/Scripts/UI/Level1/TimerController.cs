using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    //- Este script controla el tiempo de todo el nivel, no se resetea a menos que reinicies la escena y le tranfiere informacion al time del canvas
    [SerializeField] private float timeWait = 500f;
    [SerializeField] private float minTimeForPickBooster = 200f;
    [SerializeField] SpawnPlayerController spawnPlayerController;
    public bool pickBooster = false;
    public bool rudolfIsAttacking;
    public bool isAttacking;
    public bool descountTime;
    private int enemiesAttacking;
    public Image image;
    public Slider time;
    public Transform playerSC;
    public AudioSource clock;
    public AudioClip clockSlow, clockFast;
    private float currTimeWait;
    private float stopPlay = 1f;
    public float speed;

    public int EnemiesAttacking
    {
        set { enemiesAttacking += value; }
        get { return enemiesAttacking; }
    }
    public float GetCurrTme()
    {
        return currTimeWait;
    }
    public void AddTime(float time)
    {
        currTimeWait += time;
        if(currTimeWait > timeWait)
        {
            currTimeWait = timeWait;
        }
    }
    public void SubstractTime(float amount)
    {
        currTimeWait -= amount;
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
        speed = 1f;
        currTimeWait = timeWait;
        time.value = timeWait;
    }
    void Update()
    {
        if (Input.GetButton("ReloadTime"))
        {
            currTimeWait = timeWait;
        }

        if (stopPlay <= 0)
        {
            isAttacking = false;
            stopPlay = 2f;
            descountTime = false;

        }

        if (pickBooster && currTimeWait <= minTimeForPickBooster)
        {
            currTimeWait += 50;
            pickBooster = false;
        }

        if (enemiesAttacking >= 1)
        {
            currTimeWait -= Time.deltaTime * 2;
            if (clock.clip != clockFast)
            {
                clock.clip = clockFast;
                clock.Play();
            }
        }
        else if (rudolfIsAttacking)
        {
            currTimeWait -= Time.deltaTime * 60;
            if (clock.clip != clockFast)
            {
                clock.clip = clockFast;
                clock.Play();
            }
        }
        else if (isAttacking)
        {
            stopPlay -= Time.deltaTime;
            if (clock.clip != clockFast)
            {
                if (descountTime)
                {
                    AddTime(-50);
                    descountTime = false;
                }
                clock.clip = clockFast;
                clock.Play();
            }
        }
        //else if (!isAttacking && enemiesAttacking < 1)
        //{
        //    currTimeWait -= Time.deltaTime;
        //    if (clock.clip != clockSlow)
        //    {
        //        clock.clip = clockSlow;
        //        clock.Play();
        //    }
        //}
        else
        {
            currTimeWait -= Time.deltaTime * speed;
            if(clock.clip != clockSlow)
            {
                clock.clip = clockSlow;
                clock.Play();
            }
        }

        time.value = Mathf.Lerp(time.value, currTimeWait, 0.02f);
        
        if (currTimeWait <= 0)
        {
            if (rudolfIsAttacking)
            {
                spawnPlayerController.RespawnPlayer();
                currTimeWait = timeWait;
                rudolfIsAttacking = false;

            }
            else
            {
                SceneManager.LoadScene("Defeat");

            }
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

    public void StopTimer()
    {
        speed = 0;
        clock.Stop();
    }

    public void ResumeTimer()
    {
        speed = 1;
        clock.Play();
    }

}
