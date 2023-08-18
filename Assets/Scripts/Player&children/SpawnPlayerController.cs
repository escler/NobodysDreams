using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPlayerController : MonoBehaviour
{
    //TP2 - Caamaño Romina
    //Con este script atachado a un empty con todos los punto de spawn, se controla el reposicionamiento de los puntos y ejecuta el respawn del player
    //Se utiliza los eventos para cada punto de respawn que se suscriben al mismo, desde el empty SpawnPoint, el cual tienen como hijos un sensores de entrada y de salida del player

    [SerializeField] public GameObject playerGO;
    [SerializeField] public Transform playerSpawn;
    [SerializeField] public Transform initialPos;
    [SerializeField] GameObject level3On, level1Off;
    //TP2 - Caamaño Romina - Encapsular max distance
    private float maxDistanceY = -35f;
    void Update()
    {
        if (playerGO.transform.position.y <= maxDistanceY)
        {
            RespawnPlayer();
            playerGO.GetComponent<PlayerSC>().ResetSpawnPlayer();
        }
    }
    public void InitialPosition()//Se ejecuta cuando ingresa el input de reincio en el script del player
    {
        playerSpawn.transform.position = initialPos.position;
        playerSpawn.transform.rotation = initialPos.rotation;
    }
    public void Respawn(Transform transform)//reposisicona el punto de respwan a los nuevos puntos desde su transform a medida que va colisionando el player con los sensores
    {
        playerSpawn.transform.position = transform.position;
        playerSpawn.transform.rotation = transform.rotation;
    }
    public void RespawnPlayer()//Se ejecuta cuando el player cae y pasa el limite establecido
    {
        playerGO.transform.position = playerSpawn.position;
        playerGO.transform.rotation = playerSpawn.rotation;
    }

    public void ChangeMusic()
    {
        GameObject.Find("MusicBackground").GetComponent<MusicTransition>().OnLevelChange("level2");
    }

    public void Level3On()
    {
        level3On.SetActive(true);
        level1Off.SetActive(false);
    }

    public void StopTimer()
    {
        GameObject.Find("TimerController").GetComponent<TimerController>().StopTimer();
    }

    public void ResumeTimer()
    {
        GameObject.Find("TimerController").GetComponent<TimerController>().ResumeTimer();
    }
}
