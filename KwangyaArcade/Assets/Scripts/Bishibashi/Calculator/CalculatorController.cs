using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorController : MonoBehaviour
{
    public ArrowMoving arrow;
    public Text numberText;
    public Text noticeText;
    public Text questionText;
    public Text timerText;

    public GameObject gameOverButton;
    public GameObject gameClearButton;
    public GameObject TimerText;
    private float time = 60f;

    public AudioSource rightSound;
    public AudioSource wrongSound;
    public AudioSource selectSound;

    string answer = "";
    string[] calculator =
    {
        "7", "8", "9",
        "4", "5", "6",
        "1", "2", "3",
        "equal", ".", "0"
    };

    string[] question1 =
    {
        "3x2=?", "7x4=?", "15+7=?", "50÷4=?", "36-17=?", "2+3=?", "9x3=?", "4+9=?", "17÷2=?", "4x3=?",
         "4+29=?", "17÷2=?", "9-6=?", "12x11=?", "17÷2=?", "20÷5=?", "6÷3", "5x15=?", "1+1=?", "10+24=?"
    };
    string[] answer1 =
    {
        "6", "28", "22", "12.5", "19", "5", "27", "13", "8.5", "12",
        "33", "8.5", "3", "132", "8.5", "4", "2", "75", "2", "34"
    };

    string[] question2 =
    {
        "429", "609", "210", "403", "323", "1211", "1024", "007", "123", "119",
        "369", "000", "111", "777", "4.5", "117", "1126", "615", "516", "1214"
    };

    int questionIndex;
    int answerCount = 0;
    const int gameClearCount = 12;

    // Start is called before the first frame update
    void Start()
    {
        CreateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;

            if (Input.GetKeyDown("down"))
            {
                if (arrow.moveCount == 9)
                {
                    JudgeAnswer();
                    answer = "";
                }
                else
                {
                    selectSound.Play();
                    answer += calculator[arrow.moveCount];
                }

                numberText.text = answer;
            }

            if(answerCount == gameClearCount)
            {
                TimerText.SetActive(false);
                gameClearButton.SetActive(true);
            }
        }
        else
        {
            time = 0f;
            gameOverButton.SetActive(true);
        }

        timerText.text = string.Format("{0:N2}", time);
    }

    void CreateQuestion()
    {
        questionIndex = Random.Range(0, 20);
        Debug.Log(questionIndex);

        if(answerCount % 2 == 0)
        {
            noticeText.text = "연산하라!";
            questionText.text = question1[questionIndex];
        }
        else
        {
            noticeText.text = "빨리 입력하라!";
            questionText.text = question2[questionIndex];
        }
    }

    void JudgeAnswer()
    {
        if(answerCount % 2 == 0)
        {
            if (answer == answer1[questionIndex])
            {
                answerCount++;
                CreateQuestion();
                rightSound.Play();
            }
            else
            {
                wrongSound.Play();
            }
        }
        else
        {
            if (answer == question2[questionIndex])
            {
                answerCount++;
                CreateQuestion();
                rightSound.Play();
            }
            else
            {
                wrongSound.Play();
            }
        }
    }
}