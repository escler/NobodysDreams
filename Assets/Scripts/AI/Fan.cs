using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] float speedRot, timer, force;
    float actualTimer;
    // Start is called before the first frame update
    void Start()
    {
        actualTimer = timer / 2;
    }

    // Update is called once per frame
    void Update()
    {
        actualTimer -= Time.deltaTime;

        if(actualTimer < 0)
        {
            speedRot = speedRot * -1;
            actualTimer = timer;
        }

        pivot.transform.Rotate(0, speedRot * Time.deltaTime, 0);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            print("char");
            Vector3 direction = other.gameObject.transform.position + transform.position;
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * force * Time.deltaTime, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
