using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAnim : MonoBehaviour
{
    Animator animator;
    Patrol patrol;
    Explode explode;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        patrol = GetComponentInParent<Patrol>();
        explode = GetComponentInParent<Explode>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walk", patrol.enabled);
        animator.SetBool("Explosion", explode.enabled);
    }
}
