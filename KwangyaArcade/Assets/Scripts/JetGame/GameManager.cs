using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject buttonPlay;

    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;

    public GameObject scoreUIText;
    public GameObject gameClearText;

    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
        Gameclear,
    }

    GameManagerState GMState;

    // Start is called before the first frame update
    void Start()
    {
        GMState = GameManagerState.Opening;
    }

    void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:
                buttonPlay.SetActive(true);
                gameClearText.SetActive(false);
                break;

            case GameManagerState.Gameplay:
                scoreUIText.GetComponent<GameScore>().Score = 0;

                buttonPlay.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                break;

            case GameManagerState.Gameover:
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().UnscheduleEnemySpawner();

                Invoke("ChangeToOpeningState", 8f);
                break;

            case GameManagerState.Gameclear:
                gameClearText.SetActive(true);

                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
