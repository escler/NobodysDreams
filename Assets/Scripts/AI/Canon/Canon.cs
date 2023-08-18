using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] Transform pivot, spawnProjectil;
    [SerializeField] float speedRot, timer, cdThrow, actualTimerThrow;
    float actualTimer;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        actualTimer = timer / 2;
        actualTimerThrow = cdThrow;
    }

    // Update is called once per frame
    void Update()
    {
        actualTimer -= Time.deltaTime;
        actualTimerThrow -= Time.deltaTime;

        if (actualTimer < 0)
        {
            speedRot = speedRot * -1;
            actualTimer = timer;
        }

        pivot.transform.Rotate(0, speedRot * Time.deltaTime, 0);

        if(actualTimerThrow < 0)
        {
            audioSource.Play();
            GameObject.Instantiate((UnityEngine.Object)Resources.Load("ProjectilCanon"), spawnProjectil.transform.position, spawnProjectil.transform.rotation);
            actualTimerThrow = cdThrow;
        }
    }

    void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
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
