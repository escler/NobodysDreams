using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoleItem : MonoBehaviour
{
    [SerializeField] public DialogManager dialogManager;
    GameObject matObj;
    MaterializeObjects matObjs;
    [SerializeField] GameObject rulerInEnemy, cubeInEnemy;
    [SerializeField]AudioSource cantMat;
    [SerializeField]AudioClip canMatClip;


    private void Start()
    {
        matObjs = GameObject.Find("Char").GetComponent<MaterializeObjects>();

    }

    void OnEnable()
    {
        cantMat.PlayOneShot(canMatClip);
        matObjs = GameObject.Find("Char").GetComponent<MaterializeObjects>();
        matObjs.CanMat = false;
        matObj = GetComponent<Thief>().ObjectPos.gameObject;
        if (matObj.tag == "Cube")
        {
            cubeInEnemy.SetActive(true);
        }
        else if (matObj.tag == "Ruler")
        {
            rulerInEnemy.SetActive(true);
        }
        dialogManager.ShowDialog(DialogKey.LittleGhost);
        GetComponent<Thief>().ObjectGrabbed = true;
        GetComponent<Thief>().ObjectToSteal = false;
        Destroy(matObj);
        this.enabled = false;
    }
}
