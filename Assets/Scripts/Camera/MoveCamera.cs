using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    Transform cameraPos, initialCameraPos, crouchCameraPos, armsInitial, armsCrouch, arms, armsState;
    float timer, defaultPosY, defaultPosX, defaultPosYInitialPos, defaultPosXInitialPos, defaultCrouchPosYInitialPos, defaultCrouchPosXInitialPos, timerReset;
    [SerializeField] float boobingSpeed, bobbingAmount;
    public bool dontMoveCamera;

    PlayerSC playerSC;
    GroundCheck groundCheck;
    // Start is called before the first frame update

    private void Awake()
    {
        PauseStateManager.Instance.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDestroy()
    {
        PauseStateManager.Instance.OnPauseStateChanged -= OnPauseStateChanged;
    }

    void Start()
    {
        armsInitial = GameObject.Find("InitialArmsPos").GetComponent<Transform>();
        armsCrouch = GameObject.Find("ArmsCrouchPos").GetComponent<Transform>();
        arms = GameObject.Find("rootLantern").GetComponent<Transform>();
        armsState = arms;
        playerSC = GameObject.Find("Char").GetComponent<PlayerSC>();
        cameraPos = GameObject.Find("cameraPos").GetComponent<Transform>();
        initialCameraPos = cameraPos;
        crouchCameraPos = GameObject.Find("CrouchCameraPos").GetComponent<Transform>();
        defaultPosYInitialPos = cameraPos.transform.localPosition.y;
        defaultPosXInitialPos = cameraPos.transform.localPosition.x;
        defaultCrouchPosYInitialPos = crouchCameraPos.transform.localPosition.y;
        defaultCrouchPosXInitialPos = crouchCameraPos.transform.localPosition.x;
        groundCheck = GameObject.Find("Char").GetComponentInChildren<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");


        if (playerSC.IsCrouch == false)
        {
            defaultPosX = defaultPosXInitialPos;
            defaultPosY = defaultPosYInitialPos;
            cameraPos = initialCameraPos;
            armsState = armsInitial;
        }

        if (playerSC.IsCrouch == true && groundCheck.IsGrounded)
        {
            defaultPosX = defaultCrouchPosXInitialPos;
            defaultPosY = defaultCrouchPosYInitialPos;
            cameraPos = crouchCameraPos;
            armsState = armsCrouch;

        }

        if (inputX != 0 || inputY != 0)
        {
            timer += Time.deltaTime * boobingSpeed;
            cameraPos.transform.localPosition = new Vector3(cameraPos.transform.localPosition.x + Mathf.Cos(timer) * bobbingAmount * Time.deltaTime, defaultPosY, cameraPos.transform.localPosition.z);
        }
        else
        {
            cameraPos.transform.localPosition = new Vector3(Mathf.Lerp(cameraPos.transform.localPosition.x, defaultPosX, Time.deltaTime * boobingSpeed), Mathf.Lerp(cameraPos.transform.localPosition.y, defaultPosY, Time.deltaTime * boobingSpeed), cameraPos.localPosition.z);
        }

        arms.transform.position = new Vector3(arms.transform.position.x, Mathf.Lerp(arms.transform.position.y, armsState.transform.position.y, 0.4f), arms.transform.position.z);

        if (groundCheck.IsGrounded && !dontMoveCamera)
        {
            if (timerReset < 0)
            {
                print("dontMove2");
                transform.position = new Vector3(cameraPos.transform.position.x, Mathf.Lerp(transform.position.y, cameraPos.transform.position.y, 0.4f), cameraPos.transform.position.z);
            }
            else
            {
                timerReset -= Time.deltaTime;
                transform.position = cameraPos.transform.position;
            }

        }
        else
        {
            print("dontMove");
            transform.position = cameraPos.transform.position;
            timerReset = 0.2f;
        }

    }

    private void FixedUpdate()
    {
       
    }

    private void OnPauseStateChanged(PauseState newPauseState)
    {
        enabled = newPauseState == PauseState.Gameplay;
    }
}
