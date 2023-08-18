using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float speed, rayDistance;
    [SerializeField] GameObject checker, parent;
    bool ray, dontMove, dataSended;
    // Start is called before the first frame update


    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Update()
    {
        //print(dontMove);
        ray = Physics.BoxCast(checker.transform.position, checker.transform.localScale / 2, -transform.up, out hit, transform.rotation, rayDistance, layerMask);

        if (!ray)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            parent.GetComponent<BoxCollider>().isTrigger = true;
            dataSended = false;

        }
        else if (ray && hit.collider == parent)
        {
            parent.GetComponent<BoxCollider>().isTrigger = true;
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else
        {
            parent.GetComponent<BoxCollider>().isTrigger = false;

            var thiefs = FindObjectsOfType<Thief>();
            if (!dataSended)
            {
                foreach (var thief in thiefs)
                {
                    thief.CheckPosObject(gameObject.transform);
                }
                dataSended = true;
            }

            if (parent != null)
            {
                parent.layer = 6;
            }
            else
            {
                gameObject.layer = 6;
            }

            if (hit.transform.tag == "MobilePlatforms")
            {
                transform.parent = GameObject.Find(hit.collider.gameObject.name).GetComponent<Transform>();
            }
        }
    }

    private void FixedUpdate()
    {

    }



    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -transform.up * rayDistance);
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }

}
