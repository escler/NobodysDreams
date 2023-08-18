using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorLatas : MonoBehaviour
{
    public GameObject lata, bloqueo, spawnPos;
    public float cdThrow, waitingTime, startTime;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(lata, spawnPos.transform.position, spawnPos.transform.rotation);
        timer = startTime;
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            bloqueo.SetActive(false);
            waitingTime -= Time.deltaTime;
            if(waitingTime < 0)
            {
                bloqueo.SetActive(true);
                timer = cdThrow;
                Instantiate(lata, spawnPos.transform.position, spawnPos.transform.rotation);
                waitingTime = 4f;
            }
        }
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }
    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
