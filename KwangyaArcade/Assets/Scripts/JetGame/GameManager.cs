using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject buttonPlay;

    public GameObject enemySpawner;
    public GameObject enemySpawner2;
    public GameObject enemySpawner3;
    public GameObject enemySpawner4;

    public GameObject scoreUIText;
    public GameObject gameOverButton;
    public GameObject gameClearButton;

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
                gameClearButton.SetActive(false);
                break;

            case GameManagerState.Gameplay:
                scoreUIText.GetComponent<GameScore>().Score = 0;

                buttonPlay.SetActive(false);
                playerShip.GetComponent<PlayerControl>().Init();

                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().ScheduleEnemySpawner();
                enemySpawner4.GetComponent<EnemySpawner4>().ScheduleEnemySpawner();
                break;

            case GameManagerState.Gameover:
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                enemySpawner2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
                enemySpawner3.GetComponent<EnemySpawner3>().UnscheduleEnemySpawner();
                enemySpawner4.GetComponent<EnemySpawner4>().UnscheduleEnemySpawner();

                gameOverButton.SetActive(true);
                Invoke("SceneMove", 2f);
                break;

            case GameManagerState.Gameclear:
                gameClearButton.SetActive(true);

                ClearManager.stageClear[1] = true;
                Invoke("SceneMove", 2f);
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

    public void SceneMove()
    {
        SceneManager.LoadScene("JumpingAction");
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
