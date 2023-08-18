using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMemory : MonoBehaviour
{
    [SerializeField] List<GameObject> pieces;
    GameObject firstPieceGO;
    [SerializeField] float showTime;
    [SerializeField] private GameObject portalBlueEnable, bluePiece, dialogComeBack;
    float actualTimer;
    string firstLetter, secondPiece;
    int count, puzzleCount;
    public bool showed;
    [SerializeField] Canon[] canons;
    [SerializeField] LanzadorLatas[] lanzadorLatas;
    AudioSource audioSource;
    [SerializeField] AudioClip correct, wrong, win;

    void OnEnable()
    {
        audioSource = GameObject.Find("PlayRandomAudios").GetComponent<AudioSource>();
        foreach (GameObject piece in pieces)
        {
            piece.GetComponentInChildren<Animator>().enabled = true;
        }

        actualTimer = showTime;
    }

    void Update()
    {
        if(actualTimer > 0)
            actualTimer -= Time.deltaTime;

        if(actualTimer <= 0 && !showed)
        {
            foreach(GameObject piece in pieces)
            {
                piece.GetComponentInChildren<Animator>().SetBool("Hide", true);
                
            }
            showed = true;
        }
    }

    public void HitPiece(GameObject piece, string letter)
    {
        //piece.GetComponent<PiecePuzzleMemory>().ShowPiece();

        if (count == 0)
        {
            firstLetter = letter;
            count++;
            firstPieceGO = piece;
        }
        else
        {
            if(letter == firstLetter)
            {
                print("Correcto");
                puzzleCount++;
                firstPieceGO.GetComponent<PiecePuzzleMemory>().correct = true;
                piece.GetComponent<PiecePuzzleMemory>().correct = true;
                audioSource.PlayOneShot(correct);
                if (puzzleCount == 4)
                {
                    print("Gane Puzzle");
                    portalBlueEnable.GetComponent<Collider>().enabled = true;
                    bluePiece.SetActive(true);
                    dialogComeBack.SetActive(true);
                    audioSource.PlayOneShot(win);
                    foreach (Canon canon in canons)
                    {
                        canon.enabled = false;
                    }

                    foreach(LanzadorLatas lanzador in lanzadorLatas)
                    {
                        lanzador.enabled = false;
                    }
                }
            }
            else
            {
                print("Incorrecto");
                firstPieceGO.GetComponent<PiecePuzzleMemory>().HidePiece();
                audioSource.PlayOneShot(wrong);
                piece.GetComponent<PiecePuzzleMemory>().HidePiece();
                firstPieceGO.GetComponent<PiecePuzzleMemory>().guess = true;
                piece.GetComponent<PiecePuzzleMemory>().guess = true;
            }
            count = 0;
        }
    }
}
