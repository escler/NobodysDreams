using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowChar : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == 7)
        {
            print("hola");
        }
    }
}
