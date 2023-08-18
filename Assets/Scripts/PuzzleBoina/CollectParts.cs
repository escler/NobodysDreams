using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectParts : MonoBehaviour
{
    bool part1, part2, part3;
    PuzzleBoina puzzleBoina;
    [SerializeField] GameObject objectsPuzzle;
    [SerializeField] private PuzzleBlueSquareTab puzzleBlueSquareTab;
    GameObject yellowUI, redUI, blueUI;
    // Start is called before the first frame update
    void Start()
    {
        puzzleBoina = GameObject.Find("PuzzleBoinaGO").GetComponent<PuzzleBoina>();
        yellowUI = GameObject.Find("YellowUI");
        redUI = GameObject.Find("RedUI");
        blueUI = GameObject.Find("SquareUI");

        yellowUI.SetActive(false);
        redUI.SetActive(false);
        blueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectPart(int number)
    {
        if(number == 1)
        {
            part1 = true;
            yellowUI.SetActive(true);
        }
        else if (number == 2)
        {
            part2 = true;
            redUI.SetActive(true);
        }
        else if(number == 3)
        {
            part3 = true;
            blueUI.SetActive(true);
            puzzleBlueSquareTab.finalPass = true;
        }
        
        if(part1 && part2 && part3)
        {
            puzzleBoina.Colectibles = true;
            objectsPuzzle.SetActive(true);

        }
    }
}
