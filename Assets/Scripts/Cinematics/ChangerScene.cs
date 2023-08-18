using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangerScene : MonoBehaviour
{
    public float changeTime;
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0f)
        {
            SceneManager.LoadScene("Level1 1");
        }
    }
}
