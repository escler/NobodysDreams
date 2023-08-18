using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePuzzleMemory : MonoBehaviour
{
    [SerializeField] string letter;
    public bool correct, guess, comprobate;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comprobate)
        {
            time -= Time.deltaTime;
        }

        if(time < 0)
        {
            Check();
            comprobate = false;
            time = 2f;
        }
    }

    public void ShowPiece()
    {
        GetComponentInChildren<Animator>().SetBool("Show", true);
        GetComponentInChildren<Animator>().SetBool("Hide", false);
        GetComponent<BoxCollider>().enabled = false;
        comprobate = true;
        
    }

    public void HidePiece()
    {
        GetComponentInChildren<Animator>().SetBool("Show", false);
        GetComponentInChildren<Animator>().SetBool("Hide", true);
        GetComponent<BoxCollider>().enabled = true;
    }

    public void Check()
    {
        if (GameObject.Find("MemoryPuzzle").GetComponent<PuzzleMemory>().showed == true)
            GameObject.Find("MemoryPuzzle").GetComponent<PuzzleMemory>().HitPiece(gameObject, letter);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 17 && !correct || other.gameObject.layer == 17 && !guess)
        {
            ShowPiece();
            print("entre aca");

            guess = true;
        }
    }
}
