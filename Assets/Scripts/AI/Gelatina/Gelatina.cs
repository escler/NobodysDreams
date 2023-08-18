using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gelatina : MonoBehaviour
{
    DropOnDeath dropOnDeath;
    // Start is called before the first frame update
    void Start()
    {
        dropOnDeath = GetComponent<DropOnDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoInDestroy()
    {
        if(dropOnDeath != null) 
        { 
            Instantiate(dropOnDeath.spawnObj, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
