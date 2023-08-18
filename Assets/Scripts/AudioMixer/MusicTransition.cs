using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTransition : MonoBehaviour
{
    [SerializeField] AudioMixerSnapshot startLevel, transition;
    AudioSource audioSource;
    [SerializeField]AudioClip level2, level3;
    bool makeTransition;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        transition.TransitionTo(2f);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (makeTransition)
        {
            startLevel.TransitionTo(0.1f);
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                transition.TransitionTo(2f);
                makeTransition = false;
            }
        }
    }

    public void OnLevelChange(string nameLevel)
    {
        if(nameLevel == "level2")
        {
            audioSource.clip = level2;
            audioSource.volume = .5f;
            audioSource.Play();
        }

        if (nameLevel == "level3")
        {
            audioSource.clip = level3;
            audioSource.Play();
        }
        makeTransition = true;
        timer = 0.5f;
    }
}
