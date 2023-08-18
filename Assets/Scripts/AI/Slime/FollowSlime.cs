using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSlime : MonoBehaviour
{
    [SerializeField] GameObject slime;
    ParticleSystem ps;
    float initialTimer, timer;
    [SerializeField] GameObject checker;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        initialTimer = .65f;
        timer = initialTimer;
    }



    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(slime.transform.position.x, transform.position.y, slime.transform.position.z);

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Instantiate(checker, transform.position, checker.transform.rotation);
            timer = initialTimer;
        }
    }
}
