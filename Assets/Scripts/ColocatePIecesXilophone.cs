using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ColocatePIecesXilophone : MonoBehaviour
{
    [SerializeField] GameObject piece, clavo1, clavo2, nextTrigger;
    AudioSource audioSource;
    public AudioClip key1, key2, key3;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GameObject.Find("PlayRandomAudios").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            gameObject.GetComponent<EnableBucketUI>().EnableUI();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            if (Input.GetButton("Interact"))
            {
                piece.SetActive(true);
                clavo1.SetActive(true);
                clavo2.SetActive(true);
                GameObject.Find("Puzzle - Yellow Triangle").GetComponent<PuzzleXilofono>().CheckStatusKeyPlaced();
                gameObject.GetComponent<EnableBucketUI>().DisableUI();
                gameObject.SetActive(false);
                

                if(nextTrigger != null)
                {
                    nextTrigger.SetActive(true);
                }

                if(gameObject.name == "TriggerRed")
                {
                    GameObject.Find("Puzzle - Yellow Triangle").GetComponent<PuzzleXilofono>().keyRedPlaced = true;
                    audioSource.PlayOneShot(key1);

                }else if(gameObject.name == "TriggerOrange")
                {
                    GameObject.Find("Puzzle - Yellow Triangle").GetComponent<PuzzleXilofono>().keyOrangePlaced = true;
                    audioSource.PlayOneShot(key2);
                }
                else
                {
                    GameObject.Find("Puzzle - Yellow Triangle").GetComponent<PuzzleXilofono>().keyYellowPlaced = true;
                    audioSource.PlayOneShot(key3);
                }
                GameObject.Find("Puzzle - Yellow Triangle").GetComponent<PuzzleXilofono>().CheckStatusKeyPlaced();
                gameObject.GetComponent<EnableBucketUI>().DisableUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            gameObject.GetComponent<EnableBucketUI>().DisableUI();
        }
    }
}
