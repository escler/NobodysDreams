using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalKaky : MonoBehaviour
{
    [SerializeField] GameObject parte4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            parte4.SetActive(true);
            other.gameObject.GetComponent<PlayerCollitionsBody>().canElevate = true;

            Destroy(gameObject);
        }
    }
}
