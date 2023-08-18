using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChess : MonoBehaviour
{
    [SerializeField] ChessBoard chessBoard;
    [SerializeField] private AudioSource resetSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball(Clone)")
        {
            chessBoard.ResetBoard();
            resetSound.Play();
            chessBoard.peon2B = false;
            chessBoard.torreB = false;
            chessBoard.peon3B = false;
            chessBoard.alfilB = false;
            chessBoard.caballoB = false;
        }
    }
}
