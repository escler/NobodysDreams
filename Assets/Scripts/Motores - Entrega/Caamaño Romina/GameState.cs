using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    //FINAL - Caamaño Romina - se aplican Getters y Setters / Eventos como publisher para que se suscriban a ellos
    [System.Serializable] public class PlaneModeChangedEvent : UnityEvent<PlaneMode> {}
    [SerializeField] PlaneModeChangedEvent OnPlaneModeChanged;
    //FINAL - Caamaño Romina - Encapsulando el atributo planemode, solo puede ser consultado a traves de un Getter y modificado a traves de un Setter con cierta logica de control
    private PlaneMode planeMode = PlaneMode.Dream;

    //FINAL - Caamaño Romina - Encapsulamiento C# autoproperties otra forma de Getter y Setters para escribirlos en una sola linea
    public bool DreamPlaneModeEnabled { get; set; } = true;
    public bool GhostPlaneModeEnabled { get; set; } = false;
    public bool DemonPlaneModeEnabled { get; set; } = false;

    //FINAL - Caamaño Romina - Enums
    public enum PlaneMode
    {
        Dream,
        Ghost,
        Demon
    }
    public PlaneMode GetPlaneMode()//FINAL - Caamaño Romina - Getter del planemode
    {
        return planeMode;
    }
    public bool SetPlaneMode(PlaneMode planeMode)//FINAL - Caamaño Romina - Setter del planemode - estoy chequeando antes de hacer alguna modificacion si el plano que recibo es mismo en el que ya estoy o si esta habilitado
    {
        if (planeMode == this.planeMode)
            return false;
        if (planeMode == PlaneMode.Dream && !DreamPlaneModeEnabled)
            return false;
        if (planeMode == PlaneMode.Ghost && !GhostPlaneModeEnabled)
            return false;
        if (planeMode == PlaneMode.Demon && !DemonPlaneModeEnabled)
            return false;

        this.planeMode = planeMode;// si pasa esos chequeos, hace el cambio

        OnPlaneModeChanged.Invoke(this.planeMode);// y llama a los eventos por plano
        return true;
    }

    //FINAL - Caamaño Romina - SWITCHEO DE PLANOS
    public void SetNextPlaneMode()
    {
        switch (planeMode)
        {
            case PlaneMode.Dream:
                if (!SetPlaneMode(PlaneMode.Ghost))
                    SetPlaneMode(PlaneMode.Demon);
                break;
            case PlaneMode.Ghost:
                if (!SetPlaneMode(PlaneMode.Demon))
                    SetPlaneMode(PlaneMode.Dream);
                break;
            case PlaneMode.Demon:
                if (!SetPlaneMode(PlaneMode.Dream))
                    SetPlaneMode(PlaneMode.Ghost);
                break;
        }
    }
    public void SetPrevPlaneMode()
    {
        switch (planeMode)
        {
            case PlaneMode.Dream:
                if (!SetPlaneMode(PlaneMode.Demon))
                    SetPlaneMode(PlaneMode.Ghost);
                break;
            case PlaneMode.Ghost:
                if (!SetPlaneMode(PlaneMode.Dream))
                    SetPlaneMode(PlaneMode.Demon);
                break;
            case PlaneMode.Demon:
                if (!SetPlaneMode(PlaneMode.Ghost))
                    SetPlaneMode(PlaneMode.Dream);
                break;
        }
    }
}
