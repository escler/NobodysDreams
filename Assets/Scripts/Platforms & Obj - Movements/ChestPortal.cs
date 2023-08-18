using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPortal : MonoBehaviour
{
    public GameObject particlePortal;

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "KakyPeluche")
        {
            collision.gameObject.SetActive(false);
            particlePortal.SetActive(true);
        }
        if (collision.gameObject.name == "Char")
        {
            collision.gameObject.SetActive(false);
            particlePortal.SetActive(true);
        }
    }
}
