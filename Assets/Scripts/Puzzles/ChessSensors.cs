using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSensors : MonoBehaviour
{
    [SerializeField] private AudioSource lockSound;
    public ChessBoard chessBoard;
    public GameObject torreBpart, peon1Bpart, peon2Bpart, alfilBpart, caballoBpart, peon3Bpart;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Torre 1-4" && gameObject.name == "Casillero  1-1")
        {
            lockSound.Play();
            chessBoard.torreB = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            torreBpart.SetActive(false);
        }
        else if (other.name == "Peon 2-3" && gameObject.name == "Casillero 1-2")
        {
            lockSound.Play();
            chessBoard.peon1B = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            peon1Bpart.SetActive(false);
        }
        else if (other.name == "Peon 5-3" && gameObject.name == "Casillero  5-2")
        {
            lockSound.Play();
            chessBoard.peon2B = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            peon2Bpart.SetActive(false);
        }
        else if(other.name == "Alfil 4-3" && gameObject.name == "Casillero  6-1")
        {
            lockSound.Play();
            chessBoard.alfilB = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            alfilBpart.SetActive(false);
        }
        else if(other.name == "Caballo 8-3" && gameObject.name == "Casillero  7-1")
        {
            lockSound.Play();
            chessBoard.caballoB = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            caballoBpart.SetActive(false);
        }
        else if(other.name == "Peon 7-4" && gameObject.name == "Casillero  7-2")
        {
            lockSound.Play();
            chessBoard.peon3B = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            peon3Bpart.SetActive(false);
        }
    }
    private void Update()
    {
        if (chessBoard.resetParticle)
        {
            peon3Bpart.SetActive(true);
            caballoBpart.SetActive(true);
            alfilBpart.SetActive(true);
            peon2Bpart.SetActive(true);
            peon1Bpart.SetActive(true);
            torreBpart.SetActive(true);
            chessBoard.resetParticle = false;
        }
    }
}
