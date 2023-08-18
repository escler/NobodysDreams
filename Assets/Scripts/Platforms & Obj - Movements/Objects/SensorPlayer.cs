using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SensorPlayer : MonoBehaviour
{
    [SerializeField] UnityEvent TriggerEventEnter;
    [SerializeField] UnityEvent TriggerEventExit;

    [SerializeField] UnityEvent ColliderEventEnter;
    [SerializeField] UnityEvent ColliderEventExit;
    private void OnTriggerEnter(Collider other)
    {
        PlayerSC playerC = other.GetComponent<PlayerSC>();

        if (playerC != null)
        {
            TriggerEventEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerSC playerC = other.GetComponent<PlayerSC>();

        if (playerC != null)
        {
            TriggerEventExit.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerSC playerC = collision.gameObject.GetComponent<PlayerSC>();

        if (playerC != null)
        {
            ColliderEventEnter.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        PlayerSC playerC = collision.gameObject.GetComponent<PlayerSC>();

        if (playerC != null)
        {
            ColliderEventExit.Invoke();
        }
    }
}
