using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    float posZ;
    [SerializeField]Color red, blue;
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();  
    }
    void Update()
    {
        var main = ps.main;
        if(gameObject.transform.position.z < posZ)
        {
            main.startColor = red;
        }
        else
        {
            main.startColor = blue;
        }
        posZ = gameObject.transform.position.z;
    }
}
