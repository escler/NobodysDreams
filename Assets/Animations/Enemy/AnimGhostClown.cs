using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimGhostClown : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Walk", GetComponent<Patrol>().enabled);
        animator.SetBool("Run", GetComponent<ChaseCharacter>().enabled);
        animator.SetBool("Attack", GetComponent<ghAttack>().enabled);
    }
}
