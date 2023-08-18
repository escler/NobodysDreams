using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] public ChessPiece[] chessPieces;
    public bool torreB, peon1B, peon2B, alfilB, caballoB, peon3B, resetParticle;
    public GameObject letterGO, victoryBoardGO, rudolfGO, buttonResetGO;
    public DialogManager dialogManager;

    public void ResetBoard()
    {
        foreach (ChessPiece chessPiece in chessPieces)
        {
            chessPiece.ResetPosition();
        }
        resetParticle = true;
    }

    private void Update()
    {
        if (peon1B && peon2B && peon3B && torreB && alfilB && caballoB)
        {
            letterGO.SetActive(false);
            buttonResetGO.SetActive(false);
            dialogManager.ShowDialog(DialogKey.Searching);
            victoryBoardGO.SetActive(true);
            rudolfGO.SetActive(true);
            peon1B = false;
        }
    }
}
