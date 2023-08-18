using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoinaMec : MonoBehaviour
{
    [SerializeField] GameObject boina;
    [SerializeField] Transform spawnPos;
    MaterializeObjects matObjs;
    bool cantUse;
    // Start is called before the first frame update
    void Start()
    {
        matObjs = GetComponent<MaterializeObjects>();
    }

    public bool CantUse
    {
        set { cantUse = value; }
        get { return cantUse; }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Boina") && !cantUse && matObjs.materializanding == false)
        {
            Instantiate(boina, spawnPos.position, GameObject.Find("PlayerCam").GetComponent<Transform>().transform.rotation);
            cantUse = true;
        }
    }
}
