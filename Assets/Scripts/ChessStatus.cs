using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessStatus : MonoBehaviour
{
    public bool caballo, torre, alfil;
    [SerializeField] GameObject letter, caballoUI, alfilUI, torreUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckStatusPiecesChess()
    {
        if(caballo && torre & alfil)
        {
            letter.SetActive(true);
            caballoUI.SetActive(false);
            alfilUI.SetActive(false);
            torreUI.SetActive(false);

        }
    }
}
