using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsMode : MonoBehaviour
{
    private void Start()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
}
