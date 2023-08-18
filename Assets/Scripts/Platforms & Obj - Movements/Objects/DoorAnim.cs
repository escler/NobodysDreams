using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    public Animator animatorDoor;
    public AudioSource DoorSound;

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    public void EventAnimOpenDoor()
    {
        animatorDoor.Play("Open");
        DoorSound.Play();
    }

    public void EventAnimCloseDoor()
    {
        animatorDoor.Play("Close");
        DoorSound.Play();
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
