using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollitionsBody : MonoBehaviour
{
    [Header("CONTROLLERS")]
    [SerializeField] public GameState gameState;
    [SerializeField] public FlashLight flashLightSC;
    [SerializeField] public TimerController timerController;
    [SerializeField] public TutorialPaperSC tutorialPaperBool;
    [SerializeField] public DialogManager dialogManager;
    [SerializeField] public Puzzle2 puzzle2;
    [SerializeField] public Animator reloj, windowClosed, windowClosed2, windowOpen;
    [SerializeField] public AudioSource openTutorial, pickUp, booster, ClickLamp;
    [SerializeField] private Collider capC, boosterC;
    
    [Header("BOOLS")]
    public bool ballEnable = false;
    public bool iHaveCap = false;

    [Header("GAME OBJECTS")]
    [SerializeField] public GameObject psObject;
    [SerializeField] public GameObject introGO, glassesGO, letterOpenGO, nose1GO, rudolfClownGO, kakiLevel3GO, buttonResetGO, whitePiecesGO, flashLightPickGO, baloonGO, finalGO;
    [SerializeField] public GameObject cap, rullerPick, door, rubbers, buckets, ballBucket, dientesEnMano, interactiveButton, collectPickeable, monster, redKey, clavoRed1, clavoRed2, triggerOrange, orangeKey, clavoOrange1, clavoOrange2, triggerYellow, yellowKey, clavoYellow1, clavoYellow2, dientes,linterna;

    [Header("GAME OBJECTS - UI")]
    [SerializeField] public GameObject[] pickeablesUI;
    [SerializeField] public GameObject IconFantasma;
    [SerializeField] public GameObject IconFantasmaLinterna, flashLigthUI, canvasBallCount, dialogSystem;

    [Header("GAME OBJECTS - Levels On/Off")]
    [SerializeField] public GameObject level1Enable;
    [SerializeField] public GameObject level2Enable, segundaParteLevel3, parte2, parte3, parte4, parte5, finalPlat;

    [Header("GAME OBJECTS - Lights On/Off")]
    [SerializeField] public GameObject light1;
    [SerializeField] public GameObject light2, light3, Light4, lightBed, ligthPractice, ligthKaki, flashLigthArm, enableLightPuzzle3;

    public bool canElevate;

    //Private
    private bool objEnable = false;
    private bool capEnable = false;
    private bool firstTimeGrab = false;
    private bool introB = false;
    private bool enableUp;
    private bool canInteractWithItem;
    private float addTime = 25f;
    private float waitTime = 7f;
    private float balloonposY;
    float timer;
    bool canUseDientes;

    [SerializeField] Transform xilophone;

    private void OnTriggerEnter(Collider other)
    {   // Level 1 comprobaciones
        if (other.name == "Boina")
        {
            pickeablesUI[0].SetActive(true);
            canInteractWithItem = true;
        }
        else if (other.name == "Model&Collider" && !tutorialPaperBool.anyTutorialOpen)
        {
            pickeablesUI[1].SetActive(true);
            canInteractWithItem = true;
        }//pickeables
        else if (other.name == "RulerPickeable" && !tutorialPaperBool.anyTutorialOpen)
        {
            pickeablesUI[2].SetActive(true);
            canInteractWithItem = true;
        }//pickeables
        else if (other.name == "Glasses" && !tutorialPaperBool.anyTutorialOpen)
        {
            pickeablesUI[3].SetActive(true);
            canInteractWithItem = true;
        }//pickeables
        else if (other.name == "SensorPlayer - Glasses")
        {
            dialogManager.ShowDialog(DialogKey.Glasses);
        }//dialogo
        else if (other.name == "BallBucket" && GetComponent<PlayerSC>().canThrowBall)
        {
            other.gameObject.GetComponent<EnableBucketUI>().EnableUI();
            canInteractWithItem = true;
        }
        else if (other.name == "zZz")
        {
            reloj.enabled = true;
            timerController.AddTime(addTime);
            other.gameObject.SetActive(false);
            Instantiate(collectPickeable, other.gameObject.transform.position + transform.forward * 2f, other.gameObject.transform.rotation);
            booster.Play();
        }
        else if (other.name == "zZz1")
        {
            reloj.enabled = true;
            dialogManager.ShowDialog(DialogKey.Boosters);
            timerController.AddTime(addTime);
            other.gameObject.SetActive(false);
            rubbers.SetActive(true);

            Instantiate(collectPickeable, other.gameObject.transform.position + transform.forward * 2f, other.gameObject.transform.rotation);
            booster.Play();
        }
        else if (other.name == "CheckPoint7 - GhostB")
        {
            dialogManager.ShowDialog(DialogKey.Ghost);
        }
        else if (other.name == "CheckPoint8 - Candy")
        {
            dialogManager.ShowDialog(DialogKey.CandyWheel);
        }
        else if (other.name == "Pills Pickeable")
        {
            dialogManager.ShowDialog(DialogKey.BottleOfPills);
            booster.Play();
        }
        else if (other.name == "SensorPlayer - WindowWrong2")
        {
            dialogManager.ShowDialog(DialogKey.FakeWindow);
            windowClosed.Play("WindowLeft");
        }
        else if (other.name == "SensorPlayer - WindowWrong")
        {
            dialogManager.ShowDialog(DialogKey.FakeWindow);
            windowClosed2.Play("WindowMiddle");
        }
        else if (other.name == "SensorPlayer - WindowCorrect")
        {
            dialogManager.ShowDialog(DialogKey.RealWindow);
            windowOpen.Play("WindowRight");
        }

        // Level 2 comprobaciones
        else if (other.name == "zZz - BoardRotation")
        {
            reloj.enabled = true;
            dialogManager.ShowDialog(DialogKey.BoardRotation);
            timerController.AddTime(addTime);
            other.gameObject.SetActive(false);
            rubbers.SetActive(true);
            Instantiate(collectPickeable, other.gameObject.transform.position + transform.forward * 2f, other.gameObject.transform.rotation);
            booster.Play();
        }
        else if (other.name == "SabanaLvl1Off")
        {
            dialogManager.ShowDialog(DialogKey.Level2);
            level1Enable.SetActive(false);
            other.gameObject.GetComponent<Collider>().enabled = false;
        }
        else if (other.name == "SabanaLvl2Off")
        {
            level2Enable.SetActive(false);
            other.gameObject.GetComponent<Collider>().enabled = false;
        }
        else if (other.name == "LetterPickeable")
        {
            other.gameObject.SetActive(false);
            letterOpenGO.SetActive(true);
            whitePiecesGO.SetActive(true);
            buttonResetGO.SetActive(true);
            dialogManager.ShowDialog(DialogKey.EnableLetter);
        }
        else if (other.name == "Son fantasmales")
        {
            other.gameObject.SetActive(false);
            dialogManager.ShowDialog(DialogKey.Pieces);
        }
        else if (other.name == "Dientes")
        {
            canUseDientes = true;
            tutorialPaperBool.showTutorialTeeth = true;
            Destroy(other.gameObject);
            booster.Play();
        }
        else if (other.name == "arriba")
        {
            SceneManager.LoadScene("CinematicaVictoria");
        }
        else if (other.name == "Cielo")
        {
            enableUp = false;
            SceneManager.LoadScene("CinematicaVitoria");
        }
        else if (other.name == "BallPickable" && !tutorialPaperBool.anyTutorialOpen)
        {
            canInteractWithItem = true;
            other.gameObject.GetComponent<EnableBucketUI>().EnableUI();
        }
        //else if (other.name == "CheckPoint5 - DIALOG")
        //{
        //    dialogManager.ShowDialog(DialogKey.EnableEquip);
        //}
        if (other.name == "UmbrellaPickeable")
        {
            pickeablesUI[4].SetActive(true);
            canInteractWithItem = true;
        }
        if (other.name == "CheckPoint1 - Encender cosas despues")
        {
            segundaParteLevel3.SetActive(true);
        }
        if(other.gameObject.tag == "UmbrellasPick")
        {
            GetComponentInChildren<Umbrella>().AddEnergy();
            Instantiate(collectPickeable, other.gameObject.transform.position + transform.forward * 2f, other.gameObject.transform.rotation);
            booster.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "Shelf - Se cae kaki")
        {
            kakiLevel3GO.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (other.gameObject.name == "apagar al monstruo")
        {
            monster.SetActive(false);
        }
        else if (other.gameObject.name == "mostrar al monstruo")
        {
            monster.SetActive(true);
            buckets.SetActive(true);
        }
        if (other.gameObject.name == "activamepasillo")
        {
            parte5.SetActive(true);
        }
        if(other.gameObject.tag == "InteractuableDientes" && canUseDientes)
        {
            canInteractWithItem = true;
            other.transform.GetChild(0).gameObject.SetActive(true);
            dientes.SetActive(true);
            //linterna.SetActive(false);
            //flashLigthArm.SetActive(false);
        }
        if (other.gameObject.name == "XilofonoPivot")
        {
            gameObject.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Boina")
        {
            if (canInteractWithItem && Input.GetButton("Interact"))
            {
                PauseGame();
                tutorialPaperBool.showTutorialBeret = true;
                tutorialPaperBool.anyTutorialOpen = true;
                capEnable = true;
                cap.SetActive(false);//Se desactiva boina
                flashLightPickGO.SetActive(true);//Se activa la LINTERNA
                IconFantasmaLinterna.SetActive(true);//UI de PLANE DREAM
                dialogSystem.SetActive(true);//Se activa SISTEMA DE DIALOGO
                dialogManager.ShowDialog(DialogKey.IntroductionGuide);// bool dialogo
                GetComponent<BoinaMec>().enabled = true;
                pickeablesUI[0].SetActive(false);
                pickUp.Play();//Sonido de PICKEABLE
                light3.SetActive(false);
                Light4.SetActive(true);
                ClickLamp.Play();
            }
        }

        if (other.name == "RulerPickeable")
        {
            if (canInteractWithItem && Input.GetButton("Interact") && !tutorialPaperBool.anyTutorialOpen)
            {
                PauseGame();
                tutorialPaperBool.showTutorialMat1 = true;//booleano del Script tutorial REGLA
                objEnable = true;//booleano para I HAVE CAP
                tutorialPaperBool.anyTutorialOpen = true;

                other.gameObject.SetActive(false);//ruller pickeable
                light2.SetActive(false);//Se activa puerta
                light3.SetActive(false);//Se activa puerta
                lightBed.SetActive(false);
                door.SetActive(true);//Se activa puerta
                light1.SetActive(false);//Se activa puerta
                pickUp.Play();//Sonido de PICKEABLE
                pickeablesUI[2].SetActive(false);
            }
        }

        if(other.name == "Model&Collider")
        {
            if (canInteractWithItem && Input.GetButton("Interact") && !tutorialPaperBool.anyTutorialOpen)
            {
                PauseGame();
                tutorialPaperBool.showTuturialFlash = true;//booleano del Script tutorial LINTERNA
                flashLightSC.hasFlashlight = true;//booleano del Script flashligth
                tutorialPaperBool.anyTutorialOpen = true;

                flashLightPickGO.SetActive(false);//linterna pickeable
                flashLigthUI.SetActive(true);//UI de linterna
                flashLigthArm.SetActive(true);//linterna del brazo
                rullerPick.SetActive(true);//Se activa RULLER
                pickeablesUI[1].SetActive(false);

                pickUp.Play();//Sonido de PICKEABLE

                
                Light4.SetActive(false);
                light1.SetActive(true);
                ClickLamp.Play();
            }
        }

        if(other.name == "Glasses")
        {
            if (canInteractWithItem && Input.GetButton("Interact"))
            {
                PauseGame();

                tutorialPaperBool.showTutorialGlasses = true;//Booleano del Script tutorial ANTEOJOS
                gameState.GhostPlaneModeEnabled = true;//Activa PLANE GHOST
                tutorialPaperBool.anyTutorialOpen = true;

                glassesGO.SetActive(false);//anteojos pickeable
                IconFantasma.SetActive(true);//UI PLANE GHOST
                level2Enable.SetActive(true);//Se activa LEVEL2
                pickeablesUI[3].SetActive(false);

                pickUp.Play();//Sonido de PICKEABLE
            }
        }

        if (other.name == "BallBucket")
        {
            if (canInteractWithItem && Input.GetButton("Interact"))
            {
                firstTimeGrab = true;
                if (GetComponent<PlayerSC>().PickupBalls(5))
                    pickUp.Play();
            }
        }

        if(other.name == "BallPickable")
        {
            if(canInteractWithItem && Input.GetButton("Interact"))
            {
                PauseGame();
                GetComponent<PlayerSC>().PickupBalls(1);
                other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
                //Activa boleanos
                ballEnable = true;
                tutorialPaperBool.anyTutorialOpen = true;
                tutorialPaperBool.showTutorialBall = true;
                //Activa/Desactiva gameobject
                ballBucket.SetActive(true);
                canvasBallCount.SetActive(true);
                ligthPractice.SetActive(true);
                ligthKaki.SetActive(false);
                other.gameObject.SetActive(false);
                //Play
                pickUp.Play();
                Destroy(psObject);
            }
        }

        if(other.name == "UmbrellaPickeable")
        {
            if(canInteractWithItem && Input.GetButton("Interact"))
            {
                PauseGame();
                gameObject.GetComponentInChildren<Umbrella>().enabled = true;
                pickUp.Play();
                //Tutorial,et
                pickeablesUI[4].SetActive(false);
                Destroy(other.gameObject);
                tutorialPaperBool.showTutorialUmbrella = true;//booleano del Script tutorial ANTEOJOS
                tutorialPaperBool.anyTutorialOpen = true;
            }
        }

        if (other.gameObject.tag == "InteractuableDientes")
        {
            if(canInteractWithItem && Input.GetButton("Interact") && canUseDientes)
            {
                other.transform.GetChild(0).gameObject.SetActive(true);
                dientes.SetActive(false);
                linterna.SetActive(true);
                Destroy(other.gameObject);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "RulerPickeable")
        {
            pickeablesUI[2].SetActive(false);
            canInteractWithItem = false;
        }

        if(other.name == "Boina")
        {
            pickeablesUI[0].SetActive(false);
            canInteractWithItem = false;
        }

        if(other.name == "Model&Collider")
        {
            pickeablesUI[1].SetActive(false);
            canInteractWithItem = false;
        }

        if (other.name == "Glasses")
        {
            pickeablesUI[3].SetActive(false);
            canInteractWithItem = false;
        }

        if(other.name == "BallBucket")
        {
            other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
            canInteractWithItem = false;
        }

        if(other.name == "BallPickable")
        {
            other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
            canInteractWithItem = false;
        }

        if (other.name == "UmbrellaPickeable")
        {
            pickeablesUI[4].SetActive(false);
            canInteractWithItem = false;
        }

        if (other.gameObject.tag == "InteractuableDientes")
        {
            canInteractWithItem = false;
            other.transform.GetChild(0).gameObject.SetActive(false);
        }

        /*if (other.gameObject.name == "TriggerRed")
        {
            other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
        }
        if (other.gameObject.name == "TriggerOrange")
        {
            other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
        }
        if (other.gameObject.name == "TriggerYellow")
        {
            other.gameObject.GetComponent<EnableBucketUI>().DisableUI();
        }*/

        if (other.gameObject.name == "XilofonoPivot")
        {
            gameObject.transform.parent = null;
        }

        if (other.gameObject.tag == "InteractuableDientes")
        {

            other.transform.GetChild(0).gameObject.SetActive(false);
            dientes.SetActive(false);
            //flashLigthArm.SetActive(true);
            
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ConcreteF - chocolatesdown")
        {
            dialogManager.ShowDialog(DialogKey.ChocolatesDown);
        }
        else if (collision.gameObject.name == "ConcreteF - EnableLight")
        {
            enableLightPuzzle3.SetActive(true);
            dialogManager.ShowDialog(DialogKey.ShouldDown);
        }
        else if (collision.gameObject.name == "AlmohadaL-Intro")
        {
            introB = true;
            introGO.SetActive(true);
            GameObject.Find("Puzzle3").GetComponent<Puzzle3>().PlayClownSound();
            dialogManager.ShowDialog(DialogKey.QuestClowns);
            collision.gameObject.name = "AlmohadaL";
        }
        else if (collision.gameObject.name == "Floor&Columns - disable")
        {
            rudolfClownGO.SetActive(true);
        }
        else if (collision.gameObject.name == "Piso - bookBlocked")
        {
            dialogManager.ShowDialog(DialogKey.BookLocked);
        }
        else if (collision.gameObject.name == "ConcreteF - oreosRotation")
        {
            dialogManager.ShowDialog(DialogKey.OreosRotation);
        }
        else if (collision.gameObject.name == "piso1 - DuckWay")
        {
            dialogManager.ShowDialog(DialogKey.DuckWay);
        }
        else if (collision.gameObject.name == "OreoL - ToTheBed")
        {
            dialogManager.ShowDialog(DialogKey.ToTheBed);
        }
        else if (collision.gameObject.name == "caja - SearchRudolf")
        {
            dialogManager.ShowDialog(DialogKey.SearchRudolf);
            finalGO.SetActive(false);
        }
        else if (collision.gameObject.name == "piso - DownDoor")
        {
            dialogManager.ShowDialog(DialogKey.DownDoor);
        }
        else if (collision.gameObject.name == "DoorPlatform - LetsGo")
        {
            dialogManager.ShowDialog(DialogKey.GoToTheFinal);
        }
        else if (collision.gameObject.name == "Shelf - Se cae kaki")
        {
            kakiLevel3GO.GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (collision.gameObject.name == "ACTIVAR PARTE 2")
        {
           parte2.SetActive(true);
        }
        else if (collision.gameObject.name == "3er tramo")
        {
            parte3.SetActive(true);
        }
    }

    private void Update()
    {
        if (objEnable && capEnable)
        {
            iHaveCap = true;//booleano cuando tiene la gorra
            capEnable = false;
        }
        if (!puzzle2.practice1 && !puzzle2.practice2 && !puzzle2.practice3 && firstTimeGrab)
        {
            dialogManager.ShowDialog(DialogKey.Practice);
            puzzle2.practice3 = true;
        }
        if (introB && introGO)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0f)
            {
                introGO.SetActive(false);
                introB = false;
                nose1GO.SetActive(true);
                dialogManager.ShowDialog(DialogKey.NoseAdivination);
            }
        }

        if(timer > 0)
        {
            timer = -Time.deltaTime;
        }

        balloonposY = baloonGO.transform.position.y;
        if (enableUp)
        {
            //
        }
    }

    void PauseGame()
    {
        PauseState currentPauseState = PauseStateManager.Instance.CurrentPauseState;
        PauseState newPauseState = currentPauseState == PauseState.Paused
            ? PauseState.Gameplay
            : PauseState.Paused;

        PauseStateManager.Instance.SetState(newPauseState);
    }

    public void FixCamera()
    {
        GameObject.Find("CameraHolder").GetComponent<MoveCamera>().dontMoveCamera = true;
    }

    public void ResetCamera()
    {
        GameObject.Find("CameraHolder").GetComponent<MoveCamera>().dontMoveCamera = false;
    }
}
