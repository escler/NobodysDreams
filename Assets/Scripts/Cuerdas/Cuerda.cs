using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cuerda : MonoBehaviour
{
    [SerializeField] Rigidbody rbBalanza;
    [SerializeField] PuzzleEnderezarPlataforma puzzleEnderezarPlataforma;
    AudioSource audioSource;
    [SerializeField] AudioClip cut;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GameObject.Find("PlayRandomAudios").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    private void OnDestroy()
    {
        if(audioSource != null)
            audioSource.PlayOneShot(cut);
        if(rbBalanza != null)
        {
            rbBalanza.isKinematic = false;
        }

        if(puzzleEnderezarPlataforma != null)
        {
            puzzleEnderezarPlataforma.cutRope = true;
        }

    }
}
