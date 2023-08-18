using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMemory : MonoBehaviour
{
    [SerializeReference] PuzzleMemory pz;
    public void StartMemoryGame()
    {
        pz.enabled = true;
    }
}
