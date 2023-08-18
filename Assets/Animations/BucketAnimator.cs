using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketAnimator : MonoBehaviour
{
    Animator bucketAnimator;
    public BoxCollider bc;
    public CapsuleCollider cc;
    bool on;
    private void Start()
    {
        bucketAnimator = GetComponent<Animator>();
    }

    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        if (planeMode == GameState.PlaneMode.Ghost)
        {
            on = true;
        }
        else if (planeMode == GameState.PlaneMode.Dream)
        {
            on = false;
        }
    }

    private void Update()
    {
        if (GameObject.Find("Char").GetComponent<PlayerCollitionsBody>().ballEnable == true)
        {
            if (on)
            {
                bucketAnimator.SetBool("On", true);
            }
            else
            {
                bucketAnimator.SetBool("On", false);
            }
        }
    }

    public void ActivateCollider()
    {
        bc.enabled = true;
        cc.enabled = true;
    }

    public void DeactiveCollider()
    {
        bc.enabled = false;
        cc.enabled = false;

        var uiBuckets = FindObjectsOfType<EnableBucketUI>();

        foreach (var uiBucket in uiBuckets)
        {
            uiBucket.DisableUI();
        }
    }
}
