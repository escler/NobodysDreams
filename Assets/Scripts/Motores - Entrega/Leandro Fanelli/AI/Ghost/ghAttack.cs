using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghAttack : MonoBehaviour
{
    [SerializeField] float initialTimer, maxDistanceForAttack;
    public float timer;
    public AudioSource GhostAttack;
    Ghost ghost;

    // Start is called before the first frame update

    void OnEnable()
    {
        timer = initialTimer;
        GameObject.Find("TimerController").GetComponent<TimerController>().EnemiesAttacking = 1;
        GhostAttack.Play();
    }
    void OnDisable()
    {
        GameObject.Find("TimerController").GetComponent<TimerController>().EnemiesAttacking = -1;
        ghost.GhostAttack = false;
        GhostAttack.Stop();
    }

    void Start()
    {
        ghost = GetComponent<Ghost>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(GameObject.Find("Char").GetComponent<Transform>().position.x, transform.position.y, GameObject.Find("Char").GetComponent<Transform>().position.z));

        if (ghost.Distance > maxDistanceForAttack)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            ghost.GhostAttack = false;
        }
    }
}
