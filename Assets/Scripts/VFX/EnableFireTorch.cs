using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFireTorch : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject lightGO;
    [SerializeField] private AudioSource fireSound, clockSound;
    bool clockSoundPlayed;
    void Start()
    {
       // ps.Pause();
      //audioSource = GetComponent<AudioSource>();
    }

    public void PlayFire()
    {
        ps.Play();
        if(lightGO != null)
            lightGO.SetActive(true);
        if (!clockSoundPlayed)
        {
            clockSound.Play();
            clockSoundPlayed = true;
        }
        fireSound.PlayDelayed(1f);
    }
}
