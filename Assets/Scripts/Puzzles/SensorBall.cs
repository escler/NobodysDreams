using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBall : MonoBehaviour
{
    [SerializeField] private MiniBoss miniBoss;
    [SerializeField] public GameObject cartelGO, Z1GO, Z2GO, kakiGO;
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "BowlinBall - primera" && collision.gameObject.name == "SensorGlasses")
        {
            Z1GO.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (gameObject.name == "BowlinBall - segunda" && collision.gameObject.name == "SensorGlasses" )
        {
            Z2GO.SetActive(false);
            cartelGO.SetActive(true);
            gameObject.SetActive(false);
            kakiGO.SetActive(true);
            collision.gameObject.SetActive(false);
        }

        if (gameObject.name == "BowlinBall - segunda" && collision.gameObject.name == "Compuerta tobogan")
        {
            miniBoss.gateBlueClose = true;
            miniBoss.gatePinkClose = true;;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "BowlinBall - primera" && other.gameObject.name == "SensorClose" )
        {
            miniBoss.gateGreenClose = true;
        }
        else if (gameObject.name == "BowlinBall - segunda" && other.gameObject.name == "SensorClose")
        {
            miniBoss.gateGreenClose = true;
        }
    }
}
