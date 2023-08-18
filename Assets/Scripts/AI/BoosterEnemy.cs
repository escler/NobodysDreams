using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BoosterEnemy : MonoBehaviour
{
    [SerializeField] Material blue, red, actualMaterial;
    MeshRenderer meshRenderer;
    [SerializeField] float timer, valueTime;
    public float actualTimer;
    bool cantChange, isBlue;
    AudioSource audioSource;
    [SerializeField] AudioClip good, bad;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("PlayRandomAudios").GetComponent<AudioSource>();
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = blue;
        actualTimer = timer;
        isBlue = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cantChange)
            actualTimer -= Time.deltaTime;

        if(actualTimer < 0)
        {
            if(isBlue)
            {
                meshRenderer.material = red;
                isBlue = false;
            }
            else
            {
                meshRenderer.material = blue;
                isBlue = true;
            }
            actualTimer = timer;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.layer == 18)
        {
            cantChange = true;
        }

        if (other.gameObject.tag == "Player")
        {
            if(isBlue == false)
            {
                GameObject.Find("TimerController").GetComponent<TimerController>().SubstractTime(valueTime);
                audioSource.PlayOneShot(bad);
            }
            else
            {
                GameObject.Find("TimerController").GetComponent<TimerController>().AddTime(valueTime);
                audioSource.PlayOneShot(good);
            }
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }
    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
