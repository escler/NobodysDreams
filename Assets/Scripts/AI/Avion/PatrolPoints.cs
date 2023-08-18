using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed, fallingSpeedY, fallingSpeedZ, rotationFallingSpeed, minDistance, slerpRot;
    float distance;
    int actualPoint;
    public bool enemyDown;

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        actualPoint = 0;
        minDistance = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, points[actualPoint].position);

        if (enemyDown)
        {
            transform.Translate(0, -fallingSpeedY * Time.deltaTime, fallingSpeedZ * Time.deltaTime);
            transform.GetChild(0).transform.Rotate(0, 0, rotationFallingSpeed * Time.deltaTime);

            if(transform.position.y < -200f)
            {
                Destroy(this);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, points[actualPoint].position, speed * Time.deltaTime);
            var targetRotation = Quaternion.LookRotation(points[actualPoint].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, slerpRot * Time.deltaTime);

            if(distance < minDistance)
            {
                if(actualPoint >= points.Length - 1)
                {
                    actualPoint = 0;
                }
                else
                {
                    actualPoint++;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            enemyDown = true;
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
