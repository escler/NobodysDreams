using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnim : MonoBehaviour
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
        animator.SetBool("Patrol", GetComponent<Patrol>().enabled);
        animator.SetBool("Stun", GetComponent<Ghost>().stuned);
        animator.SetBool("Attack", GetComponent<ghAttack>().enabled);
    }
}
