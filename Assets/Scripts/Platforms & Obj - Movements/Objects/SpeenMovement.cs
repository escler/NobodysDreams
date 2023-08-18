using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeenMovement : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float angularVelocity = 1f;

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void FixedUpdate()
    {
        transform.Rotate(rotationAxis * angularVelocity * Time.deltaTime);
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
