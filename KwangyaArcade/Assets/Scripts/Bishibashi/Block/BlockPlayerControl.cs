using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlockPlayerControl : MonoBehaviour
{
    public BlockGenerator blockGenerator;

    public GameObject gameOverButton;
    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject timerText;

    public AudioSource rightSound;
    public AudioSource wrongSound;
    public AudioSource clearSound;

    public Text bubbleText;
    public Text TimerText;
    private float time = 60f;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        bubbleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;

            if (count < blockGenerator.maxBlock)
            {
                if (Input.GetKeyDown("left"))
                {
                    if (blockGenerator.blockArray[count].tag == "RedTag")
                    {
                        CorrectAnswer();
                    }
                    else
                    {
                        WrongAnswer();
                    }
                }
                if (Input.GetKeyDown("down"))
                {
                    if (blockGenerator.blockArray[count].tag == "GreenTag")
                    {
                        CorrectAnswer();
                    }
                    else
                    {
                        WrongAnswer();
                    }
                }
                if (Input.GetKeyDown("right"))
                {
                    if (blockGenerator.blockArray[count].tag == "BlueTag")
                    {
                        CorrectAnswer();
                    }
                    else
                    {
                        WrongAnswer();
                    }
                }
            }

            if(count == blockGenerator.maxBlock)
            {
                timerText.SetActive(false);
                gameClearText.SetActive(true);
                Invoke("SceneMoveNextGame", 2f);
            }
        }
        else
        {
            time = 0f;
            gameOverButton.SetActive(true);
            gameOverText.SetActive(true);
            Invoke("SceneMoveHome", 2f);
        }

        TimerText.text = string.Format("{0:N2}", time);
    }

    void CorrectAnswer()
    {
        bubbleText.text = "<color=#467EDE>" + "O" + "</color>";
        Destroy(blockGenerator.blockArray[count++].gameObject);
        rightSound.Play();
    }

    void WrongAnswer()
    {
        bubbleText.text = "<color=#DE4B51>" + "X" + "</color>";
        time -= 5f;
        wrongSound.Play();
    }

    public void SceneMoveHome()
    {
        SceneManager.LoadScene("JumpingAction");
    }

    public void SceneMoveNextGame()
    {
        SceneManager.LoadScene("3.Mini2_ot");
    }
}
