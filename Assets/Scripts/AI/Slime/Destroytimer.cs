using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroytimer : MonoBehaviour
{
    bool slowChar;
    [SerializeField] float timeForDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeForDestroy);
    }

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
        if (slowChar)
        {
            GameObject.Find("Char").gameObject.GetComponent<PlayerSC>().CancelSlow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            slowChar = true;
            other.gameObject.GetComponent<PlayerSC>().Slow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            slowChar = false;
            other.gameObject.GetComponent<PlayerSC>().CancelSlow();
        }
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
