using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiecesAndCap : MonoBehaviour
{
    [SerializeField] private PuzzleXilofono puzzleXilofono;
    GameObject redUI, orangeUI, yellowUI;
    public AudioSource grabPiece;

    private void Start()
    {
        grabPiece = GameObject.Find("PickObjAudio").GetComponent<AudioSource>();
        redUI = GameObject.Find("RedKeyUI");
        yellowUI = GameObject.Find("YellowKeyUI");
        orangeUI = GameObject.Find("OrangeKeyUI");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char")
        {
            if(gameObject.name == "RedKey")
            {
                puzzleXilofono.keyRedDis = true;
                gameObject.SetActive(false);
                redUI.SetActive(true);
            }
            else if (gameObject.name == "YellowKey")
            {
                puzzleXilofono.keyYellowDis = true;
                gameObject.SetActive(false);
                yellowUI.SetActive(true);
            }
            else if (gameObject.name == "OrangeKey")
            {
                puzzleXilofono.keyOrangeDis = true;
                gameObject.SetActive(false);
                orangeUI.SetActive(true);
            }
            else if (gameObject.name == "Xilofono")
            {
                puzzleXilofono.xilofonoComplete = true;
            }

            grabPiece.Play();
        }
    }
}
