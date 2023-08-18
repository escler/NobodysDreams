using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] float speed, timer;
    float actualTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;

        if(actualTime > timer)
        {
            speed *= -1f;
            actualTime = 0;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponentInChildren<Umbrella>().DestroyUmbrella();
        }
    }
}
