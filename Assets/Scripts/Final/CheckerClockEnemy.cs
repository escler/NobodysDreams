using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerClockEnemy : MonoBehaviour
{
    [SerializeField] GameObject clockEnemy, actualClockEnemy;
    [SerializeField] Transform initialPos;
    public Transform[] patrols1, patrols2, patrols3;
    float timer;
    [SerializeField] float timeForRespawn;
    // Start is called before the first frame update
    void Awake()
    {
        timer = timeForRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(actualClockEnemy == null)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                if (gameObject.name == "CheckClock")
                {
                    actualClockEnemy = Instantiate(clockEnemy, initialPos.position, initialPos.rotation);
                    actualClockEnemy.GetComponent<Patrol>().patrolPoints = patrols1;
                }
                else if (gameObject.name == "CheckClock (1)")
                {
                    actualClockEnemy = Instantiate(clockEnemy, initialPos.position, initialPos.rotation);
                    actualClockEnemy.GetComponent<Patrol>().patrolPoints = patrols2;
                }
                if(gameObject.name == "CheckClock (2)")
                {
                    actualClockEnemy = Instantiate(clockEnemy, initialPos.position, initialPos.rotation);
                    actualClockEnemy.GetComponent<Patrol>().patrolPoints = patrols3;
                }
                timer = timeForRespawn;
            }
        }
    }
}
