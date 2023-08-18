using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerDis : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToDisable;
    [SerializeField] private GameObject[] gameObjectsToEnable;

    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            for (int i = 0; i < gameObjectsToDisable.Length; i++)
            {
                gameObjectsToDisable[i].SetActive(false);
            }

            for (int i = 0; i < gameObjectsToEnable.Length; i++)
            {
                gameObjectsToEnable[i].SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Char")
        {
            for (int i = 0; i < gameObjectsToDisable.Length; i++)
            {
                gameObjectsToDisable[i].SetActive(false);
            }

            for (int i = 0; i < gameObjectsToEnable.Length; i++)
            {
                gameObjectsToEnable[i].SetActive(true);
            }
        }
    }

}
