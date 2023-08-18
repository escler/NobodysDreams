using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMaterialized : MonoBehaviour
{
    public PlayerCollitionsBody playerC;
    bool canUseMaterialized = false;

    void Update()
    {
        if (playerC.iHaveCap)
        {
            canUseMaterialized = playerC.iHaveCap;
            playerC.iHaveCap = false;
        }
        gameObject.GetComponent<MaterializeObjects>().enabled = canUseMaterialized;
    }
    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        canUseMaterialized = planeMode == GameState.PlaneMode.Dream;
    }
}
