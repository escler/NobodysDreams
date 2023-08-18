using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColorObjects : MonoBehaviour
{
    private Color colorDreamPlane;
    [SerializeField] private Color colorGhostPlane;
    [SerializeField] private Color colorDemonPlane;

    void Awake()
    {
        colorDreamPlane = GetComponentInChildren<Renderer>().material.color;
    }
    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        switch (planeMode)
        {
            case GameState.PlaneMode.Dream:
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                    renderer.material.color = colorDreamPlane;
                break;
            case GameState.PlaneMode.Ghost:
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                    renderer.material.color = colorGhostPlane;
                break;
            case GameState.PlaneMode.Demon:
                foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                    renderer.material.color = colorDemonPlane;
                break;
        }
    }
}
