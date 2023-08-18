using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float sensX, sensY, limitAngleY;
    float xRotation, yRotation;
    Transform orientation, lantern;
    // Start is called before the first frame update
    void Start()
    {
        orientation = GameObject.Find("Char").GetComponent<Transform>();
        lantern = GameObject.Find("rootLantern").GetComponent<Transform>();
    }

    // Update is called once per frame

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseY;
        yRotation = Mathf.Clamp(yRotation, -limitAngleY, limitAngleY);

        xRotation += mouseX;

        transform.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
        orientation.rotation = Quaternion.Euler(0, xRotation, 0);
        lantern.rotation = Quaternion.Euler(-yRotation, xRotation, 0);
    }

    private void Awake()
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
