using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechanger2 : MonoBehaviour
{
    public float changeTime;
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0f)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
