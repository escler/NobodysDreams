using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PuzzleRedChessPiece : MonoBehaviour
{
    PuzzleRedCircle puzzleRedCircle;
    [SerializeField] string piece;
    AudioSource grabPiece;
    // Start is called before the first frame update
    void Start()
    {
        puzzleRedCircle = GameObject.Find("Puzzle - RedCircle").GetComponent<PuzzleRedCircle>();
        grabPiece = GameObject.Find("PickObjAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Char")
        {
            puzzleRedCircle.SpawnPieceChess(piece);
            puzzleRedCircle.CheckStatusPieces();
            grabPiece.Play();
            Destroy(gameObject);
        }
    }
}
