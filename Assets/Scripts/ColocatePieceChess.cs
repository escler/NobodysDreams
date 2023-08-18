using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocatePieceChess : MonoBehaviour
{
    [SerializeField] GameObject piece;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            gameObject.GetComponent<EnableBucketUI>().EnableUI();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            if (Input.GetButton("Interact"))
            {
                piece.SetActive(true);
                gameObject.GetComponent<EnableBucketUI>().DisableUI();
                gameObject.SetActive(false);

                if (gameObject.name == "TriggerCaballo")
                {
                    GameObject.Find("Ajedrez").GetComponent<ChessStatus>().caballo = true;

                }
                else if (gameObject.name == "TriggerTorre")
                {
                    GameObject.Find("Ajedrez").GetComponent<ChessStatus>().torre = true;
                }
                else
                {
                    GameObject.Find("Ajedrez").GetComponent<ChessStatus>().alfil = true;
                }
                GameObject.Find("Ajedrez").GetComponent<ChessStatus>().CheckStatusPiecesChess();
                gameObject.GetComponent<EnableBucketUI>().DisableUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            gameObject.GetComponent<EnableBucketUI>().DisableUI();
        }
    }
}
