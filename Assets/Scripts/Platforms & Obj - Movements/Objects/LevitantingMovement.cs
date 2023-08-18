using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitantingMovement : MonoBehaviour
{
    public float maxHeight;
    public float velocityLevitation;
    private Vector3 initialPos;
    bool boina;

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
        initialPos = transform.position;
    }
    void Update()
    {
        if (!boina)
        {
            transform.position = initialPos + new Vector3(0, Mathf.Sin(Time.time * velocityLevitation) * maxHeight, 0);
        }
        else
        {
            transform.position = transform.position;
        }

        if(gameObject.name == "Thieft - Boss2")
        {
            if(gameObject.GetComponent<GhostMovement>().moving == true)
            {
                transform.position = transform.position;
            }
            else
            {
                transform.position = initialPos + new Vector3(0, Mathf.Sin(Time.time * velocityLevitation) * maxHeight, 0);
            }
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 18)
        {
            boina = true;
        }
    }
}
