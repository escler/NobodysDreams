using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollitionBall : MonoBehaviour
{
    public AudioSource hit;
    void Start()
    {
        hit.Play();
        Destroy(gameObject, 2f);
    }

}
