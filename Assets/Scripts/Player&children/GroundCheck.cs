using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    [SerializeField] float radius = 0.1f;
    RaycastHit hit;
    bool isParent;

    bool isGrounded;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, radius, ground);
        isGrounded = colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
