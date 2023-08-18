using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleRedCircle : MonoBehaviour
{
    public bool canPortalExit, alfilPiece, torrePiece, caballoPiece;
    [SerializeField] Collider portalExitTrigger;
    [SerializeField] GameObject alfilPieceUI, torrePieceUI, caballoPieceUI, portalParticleEnable;
    public Sprite alfil, torre, caballo;
    public AudioSource iHavePieces;
    // Start is called before the first frame update
    void Awake()
    {
        alfilPieceUI.SetActive(true);
        torrePieceUI.SetActive(true);
        caballoPieceUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckStatusPieces()
    {
        if (alfilPiece && torrePiece && caballoPiece)
        {
            canPortalExit = true;
            portalExitTrigger.enabled = true;
            portalParticleEnable.SetActive(true);
            iHavePieces.Play();
        }
    }

    public void SpawnPieceChess(string name)
    {
        if(name == "Alfil")
        {
            alfilPiece = true;
            alfilPieceUI.GetComponent<Image>().sprite = alfil;
            
        }
        else if(name == "Torre")
        {
            torrePiece = true;
            torrePieceUI.GetComponent<Image>().sprite = torre;
        }
        else
        {
            caballoPiece = true;
            caballoPieceUI.GetComponent<Image>().sprite = caballo;
        }
    }
}
