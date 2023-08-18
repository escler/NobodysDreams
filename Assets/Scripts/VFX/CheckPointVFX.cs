using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointVFX : MonoBehaviour
{
    [SerializeField] GameObject torch;
    private void OnTriggerEnter(Collider other)
    {
       torch.GetComponent<EnableFireTorch>().PlayFire();
        if (gameObject.name == "CheckPoint5 - DIALOG")
        {
            torch.GetComponent<EnableFireTorch>().PlayFire();
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
