using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBall : MonoBehaviour
{
    [SerializeField] private PlayerSC playerData;
    [SerializeField] public GameState gameState;
    [SerializeField] public PlayerCollitionsBody playerC;
    [SerializeField] private int currBall;
    [SerializeField] public GameObject zeroBall, oneBall, twoBalls, threeBalls, fourBalls, fiveBalls, disable;
    private bool disableBool = false;
    void Update()
    {
        currBall = playerData.BallCount;
        if (gameState.GhostPlaneModeEnabled && playerC.ballEnable)
        {
            if (currBall == 0)
            {
                zeroBall.SetActive(true);
                oneBall.SetActive(false);
                twoBalls.SetActive(false);
                threeBalls.SetActive(false);
                fourBalls.SetActive(false);
                fiveBalls.SetActive(false);
            }
            else if (currBall == 1)
            {
                zeroBall.SetActive(false);
                oneBall.SetActive(true);
                twoBalls.SetActive(false);
                threeBalls.SetActive(false);
                fourBalls.SetActive(false);
                fiveBalls.SetActive(false);
            }
            else if (currBall == 2)
            {
                zeroBall.SetActive(false);
                oneBall.SetActive(false);
                twoBalls.SetActive(true);
                threeBalls.SetActive(false);
                fourBalls.SetActive(false);
                fiveBalls.SetActive(false);
            }
            else if (currBall == 3)
            {
                zeroBall.SetActive(false);
                oneBall.SetActive(false);
                twoBalls.SetActive(false);
                threeBalls.SetActive(true);
                fourBalls.SetActive(false);
                fiveBalls.SetActive(false);
            }
            else if (currBall == 4)
            {
                zeroBall.SetActive(false);
                oneBall.SetActive(false);
                twoBalls.SetActive(false);
                threeBalls.SetActive(false);
                fourBalls.SetActive(true);
                fiveBalls.SetActive(false);
            }
            else if (currBall == 5)
            {
                zeroBall.SetActive(false);
                oneBall.SetActive(false);
                twoBalls.SetActive(false);
                threeBalls.SetActive(false);
                fourBalls.SetActive(false);
                fiveBalls.SetActive(true);
            }
        }
        else if(playerC.ballEnable && currBall == 0)
        {
            disable.SetActive(true);
            zeroBall.SetActive(true);
            oneBall.SetActive(false);
            twoBalls.SetActive(false);
            threeBalls.SetActive(false);
            fourBalls.SetActive(false);
            fiveBalls.SetActive(false);
        }

        if (disableBool == true)
        {
            disable.SetActive(true);
        }
        else
        {
            disable.SetActive(false);
        }
    }
    public void OnPlaneModeChanged(GameState.PlaneMode planeMode)
    {
        disableBool = planeMode == GameState.PlaneMode.Dream;
    }
}
