using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float angularVelocity = 1f;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    //private void OnDestroy()
    //{
    //    PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    //}

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(angularVelocity * rotationAxis * Time.fixedDeltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    //private void OnPauseStateChanged(PauseState newPauseState)
    //{
    //    enabled = newPauseState == PauseState.Gameplay;
    //}

}
