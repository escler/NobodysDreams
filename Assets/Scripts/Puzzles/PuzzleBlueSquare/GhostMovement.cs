using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] public bool canITalk, stopMoving;
    [SerializeField] private GameObject dialog, disablesensor;
    [SerializeField] public float timeCount, smoothMov;
    [SerializeField] GameObject finalPosBoss;
    [SerializeField] Vector3 velocity = Vector3.zero;
    public bool moving;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, finalPosBoss.transform.position);

        if (canITalk)
        {
            timeCount -= Time.deltaTime;
            if(timeCount < 0)
                canITalk = false;
        }

        if (timeCount <= 0f && !stopMoving)
        {
            dialog.SetActive(false);
            moving = true;
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, finalPosBoss.transform.position, ref velocity, smoothMov * Time.deltaTime);
        }

        if (distance < 0.1f)
        {
            stopMoving = true;
            moving = true;
        }
        print(moving);
    }

    public void CanTalk()
    {
        dialog.SetActive(true);
        canITalk = true;
        disablesensor.SetActive(false);
    }
}
