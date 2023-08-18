using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockEnemy : Enemies, IEnemy
{
    MonoBehaviour patrol, explode, stayPos;
    [SerializeField] float distanceForExplode;
    float distance;
    bool explosion;

    public bool Explosion
    {
        get { return explosion; }
        set { explosion = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Behaviors();
    }

    // Update is called once per frame
    void Update()
    {
        Variables();
        Transitions();
    }

    public void Behaviors()
    {
        patrol = GetComponent<Patrol>();
        explode = GetComponent<Explode>();
    }


    public void Variables()
    {
        distance = Vector3.Distance(characterPos.position, transform.position);
    }

    public void Transitions()
    {
        if (distance > distanceForExplode && !explosion)
        {
            patrol.enabled = true;
            explode.enabled = false;
        }
        else
        {
            patrol.enabled = false;
            explode.enabled = true;
        }
    }

    public void Stunned()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 17)
        {
            print("pegue");
            explosion = true;
        }
    }
}
