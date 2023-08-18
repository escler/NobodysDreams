using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 1f;
    int enemiesAttack;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    float initialShakeAmount, initialDecreaseFactor;
    bool fallingWall;

    public float timerEvent;

    public bool FallingWall
    {
        set { fallingWall = value; }
    }

    Vector3 originalPos;

    void Awake()
    {
        initialShakeAmount = shakeAmount;
        initialDecreaseFactor = decreaseFactor;

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
            
        enemiesAttack = GameObject.Find("TimerController").GetComponent<TimerController>().EnemiesAttacking;

        if(timerEvent > 0)
        {
            timerEvent -= Time.deltaTime;
        }

        if (fallingWall)
        {
            shakeAmount = 0.02f;
            Vector3 NextPos = camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            Vector3.Lerp(camTransform.localPosition, NextPos, 1f);
        }
        else if(timerEvent > 0)
        {
            shakeAmount = 0.2f;
            Vector3 NextPos = camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount * 1.2f;
            Vector3.Lerp(camTransform.localPosition, NextPos, 1f);
        }
        else
        {
            shakeAmount = 0.01f;
            if (enemiesAttack > 0)
            {
                Vector3 NextPos = camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                Vector3.Lerp(camTransform.localPosition, NextPos, 1f);

            }
            else
            {
                shakeDuration = 0f;
                camTransform.localPosition = originalPos;
            }
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

    public void ResetVariable()
    {
        shakeAmount = initialShakeAmount;
        decreaseFactor = initialDecreaseFactor;
    }

    public void ShakeExplosion(float duration)
    {
        timerEvent = duration;
    }


}
