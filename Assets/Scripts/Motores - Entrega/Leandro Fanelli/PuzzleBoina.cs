using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FINAL - Leandro Fanelli - Se hace uso de Diccionario para dividir las posiciones de los elementos del Puzzle, y asi poder usarlos de manera mas comoda.
public class PuzzleBoina : MonoBehaviour
{
    [SerializeField] GameObject part1, part2, part3, character, door;
    [SerializeField] Transform finalPos1, finalPos2, finalPos3, correctYellow, correctRed, correctBlue;
    [SerializeField] float speed;
    Dictionary<string, Transform> yellowForm;
    Dictionary<string, Transform> redForm;
    Dictionary<string, Transform> blueForm;
    float distance1, distance2, distance3;
    int count;
    bool move1, move2, move3, correct1, correct2, correct3, colectibles;
    public bool playerIn;

    AudioSource audioSource;
    [SerializeField] AudioClip correct, wrong;
    // Start is called before the first frame update
    public bool Colectibles
    {
        get { return colectibles; }
        set { colectibles = value; }
    }

    void Start()
    {
        character = GameObject.Find("Char");
        audioSource = GetComponent<AudioSource>();

        yellowForm = new Dictionary<string, Transform>()
        {
            {"finalPos",finalPos1 },
            {"correctPos", correctYellow }
        };

        redForm = new Dictionary<string, Transform>()
        {
            {"finalPos",finalPos2 },
            {"correctPos", correctRed }
        };

        blueForm = new Dictionary<string, Transform>()
        {
            {"finalPos",finalPos3 },
            {"correctPos", correctBlue }
        };
    }

    // Update is called once per frame
    void Update()
    {
        distance1 = Vector3.Distance(part1.transform.position, finalPos1.transform.position);
        distance2 = Vector3.Distance(part2.transform.position, finalPos2.transform.position);
        distance3 = Vector3.Distance(part3.transform.position, finalPos3.transform.position);

        if (playerIn && count == 0)
        {
            part1.transform.position = Vector3.Lerp(part1.transform.position, yellowForm["finalPos"].position, speed * Time.deltaTime);
            part2.transform.position = Vector3.Lerp(part2.transform.position, redForm["finalPos"].position, speed * Time.deltaTime);
            part3.transform.position = Vector3.Lerp(part3.transform.position, blueForm["finalPos"].position, speed * Time.deltaTime);


            
            if (distance1 < 0.3f && distance2 < 0.3f && distance3 < 0.3f)
            {
                count = 1;
            }
        }

        if(count == 1)
        {
            if(move1 == true && !correct1)
            {
                part1.transform.position = Vector3.Lerp(part1.transform.position, yellowForm["finalPos"].position, speed * Time.deltaTime);

                if(distance1 < 1f)
                {
                    move1 = false;
                }
            }

            if(move2 == true && !correct2)
            {
                part2.transform.position = Vector3.Lerp(part2.transform.position, redForm["finalPos"].position, speed * Time.deltaTime);

                if (distance2 < 1f)
                {
                    move2 = false;
                }
            }

            if(move3 == true && !correct3)
            {
                part3.transform.position = Vector3.Lerp(part3.transform.position, blueForm["finalPos"].position, speed * Time.deltaTime);

                if (distance3 < 1f)
                {
                    move3 = false;
                }
            }

            if (correct1)
            {
                part1.transform.position = Vector3.Lerp(part1.transform.position, yellowForm["correctPos"].position, 2f * Time.deltaTime);
            }

            if (correct2)
            {
                part2.transform.position = Vector3.Lerp(part2.transform.position, redForm["correctPos"].position, 2f * Time.deltaTime);
            }

            if (correct3)
            {
                part3.transform.position = Vector3.Lerp(part3.transform.position, blueForm["correctPos"].position, 2f * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Char" && !playerIn && count == 0 && colectibles)
        {
            GameObject.Find("YellowUI").SetActive(false);
            GameObject.Find("RedUI").SetActive(false);
            GameObject.Find("SquareUI").SetActive(false);
            playerIn = true;
            part1.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2f, character.transform.position.z);
            part2.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2f, character.transform.position.z);
            part3.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 2f, character.transform.position.z);
            part1.GetComponentInChildren<MeshRenderer>().enabled = true;
            part2.GetComponentInChildren<MeshRenderer>().enabled = true;
            part3.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
    }

    public void Incorrect(string color)
    {
        if (color == "Yellow")
        {
            bool soundPlayed1 = false;
            if (count >= 1 && !correct1 && !soundPlayed1)
            {
                audioSource.PlayOneShot(wrong);
                soundPlayed1 = true;
            }
            move1 = true;
        }
        else if (color == "Red")
        {
            bool soundPlayed2 = false;
            if (count >= 1 && !correct2 && !soundPlayed2)
            {
                audioSource.PlayOneShot(wrong);
                soundPlayed2 = true;
            }
            move2 = true;
        }
        else if(color == "Blue")
        {
            bool soundPlayed3 = false;
            if (count >= 1 && !correct3 && !soundPlayed3)
            {
                audioSource.PlayOneShot(wrong);
                soundPlayed3 = true;
            }
            move3 = true;
        }
    }

    public void CorrectColor(string color)
    {
        audioSource.PlayOneShot(correct);
        
        if (color == "Yellow")
        {
            correct1 = true;
            part1.GetComponent<Collider>().enabled = false;
        }
        else if (color == "Red")
        {
            correct2 = true;
            part2.GetComponent<Collider>().enabled = false;
        }
        else if (color == "Blue")
        {
            correct3 = true;
        }

        if(correct1 && correct2 && correct3)
        {
            door.GetComponent<DoorAnim>().EventAnimOpenDoor();
        }
    }
}
