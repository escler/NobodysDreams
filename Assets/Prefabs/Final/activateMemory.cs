using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateMemory : MonoBehaviour
{
    [SerializeField] PuzzleMemory memoryPuzzle;
    // Start is called before the first frame update
    void Start()
    {
        memoryPuzzle = GameObject.Find("MemoryPuzzle").GetComponent<PuzzleMemory>();
    }

    private void Awake()
    {
        memoryPuzzle = GameObject.Find("MemoryPuzzle").GetComponent<PuzzleMemory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Char" && Input.GetButton("Interact"))
        {
            GameObject.Find("MemoryPuzzle").GetComponent<StartMemory>().StartMemoryGame();
        }
    }
}
