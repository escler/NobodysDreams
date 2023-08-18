using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] int partNumber;
    PuzzleBoina puzzleBoina;
    [SerializeField] string color;
    AudioSource audioSource;
    [SerializeField] GameObject ps;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("PuzzleBoinaGO") != null)
            puzzleBoina = GameObject.Find("PuzzleBoinaGO").GetComponent<PuzzleBoina>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char" && puzzleBoina.playerIn == false)
        {
            if(puzzleBoina.playerIn == true)
            {
                gameObject.transform.parent = null;
            }
            else
            {
                GameObject.Find("PuzzleBoina").GetComponent<CollectParts>().CollectPart(partNumber);
                Instantiate(ps, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.name == "Char" && puzzleBoina.playerIn == true)
        {
            puzzleBoina.Incorrect(color);
        }

        if(other.gameObject.tag == "Check")
        {

            if(color == other.gameObject.name)
            {
                print("correcto");
                puzzleBoina.CorrectColor(color);
            }
            else
            {
                gameObject.transform.parent = null;
                puzzleBoina.Incorrect(color);
            }
        }
    }
}
