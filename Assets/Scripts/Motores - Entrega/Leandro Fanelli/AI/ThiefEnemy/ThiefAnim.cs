using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnim : MonoBehaviour
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
        animator.SetBool("Stun", GetComponent<Thief>().stuned);
        animator.SetBool("Escape", GetComponent<Escape>().enabled);
        animator.SetBool("PatrolStun", GetComponent<Thief>().cantSteal);
    }
}
