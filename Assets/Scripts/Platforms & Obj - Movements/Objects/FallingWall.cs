using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    Shaking shaking;

    // Start is called before the first frame update
    void Start()
    {
        shaking = GameObject.Find("CameraHolder").GetComponent<Shaking>();
    }

    public void ActivateShake()
    {
        shaking.FallingWall = true;
    }

    public void EndFall()
    {
        shaking.ResetVariable();
        shaking.FallingWall = false;
    }
}
