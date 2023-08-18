using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LataRodante : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
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

    // Update is called once per frame
    void Update()
    {
        
    }

}
