using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource TickTack;
   
    public void PlayMusic()
    {
        TickTack.Play();
    }
    public void StopMusic()
    {
        TickTack.Stop();
    }
}
