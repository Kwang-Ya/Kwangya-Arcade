using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    public int currAlphaIdx;
    public char currAlpha;
    public int quesIdx;

    public char[] alphabets;
    public Vector3[] alphaTransform;
    public string[] questions = { "AAA", "ABC", "CNN", "CPU", "DDT",
            "FBI", "INS", "KFC", "LIN", "MON", "NMI", "NOT", "OOO", "POP",
            "RPG", "SOS", "SUN", "TBC", "TOP", "TUE", "USA", "USO", "XYZ" };
    private int quesCount;
    private int maxCount = 5;       // 맞춰야 하는 문제 수

    public Text alphabetText;
    public Text quesText;
    public Text answText;

    public Text timeText;
    public Text gameoverText;
    public Text gameclearText;
    private float playTime = 0f;
    private float clearTime = 30f;     // 클리어 해야 하는 시간

    public Transform arrow;
    public Vector3 arrowTransform;
    public Vector3 leftmostArrowTransform;      // A 칸 위치
    public Vector3 rightmostArrowTransform;      // < 칸 위치
    public Vector3 xOffset;

    void Start()
    {
        currAlphaIdx = 0;
        quesIdx = 0;
        quesCount = 0;
    //currAlpha = alphabets[currAlphaIdx];

        answText.text = "";
        xOffset = new Vector3(0.263f, 0f, 0f);
        arrowTransform = arrow.gameObject.transform.position;
        leftmostArrowTransform = arrowTransform;
        rightmostArrowTransform = arrowTransform + CalcOffset(26);

        //Debug.Log(arrowTransform);
        Initialize();
        SpawnQues();
    }

    public void Initialize()
    {
        alphabets = new char[27];
        alphaTransform = new Vector3[alphabets.Length];
        //alphabetText.text = "";

        for (int i = 0; i < alphabets.Length; i++)
        {
            if (i == 26)
                alphabets[i] = '<';
            else
                alphabets[i] = (char)(65 + i);

            //alphabetText.text += alphabets[i];
            alphaTransform[i] = new Vector3(0f, 0f, 0f) + CalcOffset(i);
        }
    }

    void Update()
    {
        playTime += Time.deltaTime;
        timeText.text = string.Format("{0:F2}", playTime);

        if (playTime > clearTime)
            GameOver();

        if (Input.GetKeyDown("left"))   // 빨 -> 왼쪽 이동
        {
            MoveCurrAlpha(-1);
            Move(CalcOffset(-1));
        }
        if (Input.GetKeyDown("down"))   // 초 -> 선택
        {
            SelectAlpha();
        }
        if (Input.GetKeyDown("right"))   // 파 -> 오른쪽 이동
        {
            MoveCurrAlpha(1);
            Move(CalcOffset(1));
        }

        arrow.gameObject.transform.position = arrowTransform;
        // raycast
    }

    private bool Move(Vector3 translation)
    {
        Vector3 newPosition = arrow.gameObject.transform.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        // 맨 오른쪽 < 위치에서 오른쪽으로 이동하면 맨 왼쪽 A 위치로
        if (newPosition.x > rightmostArrowTransform.x)
        {
            newPosition.x = leftmostArrowTransform.x;
            //currAlphaIdx = 0;
        }
        // 맨 왼쪽 A 위치에서 왼쪽으로 이동하면 맨 오른쪽 < 위치로
        else if (newPosition.x < leftmostArrowTransform.x)
        {
            newPosition.x = rightmostArrowTransform.x;
            //currAlphaIdx = alphabets.Length - 1;
        }

        // arrow를 새 위치로
        arrowTransform = newPosition;
        // 현재 가리키는 알파벳 조정
        //currAlpha = alphabets[currAlphaIdx];

        Debug.Log("current alphabet : " + currAlpha);

        return true;
    }

    Vector3 CalcOffset(int count)
    {
        Vector3 vec = xOffset * count;
       
        return vec;
    }

    void MoveCurrAlpha(int count)
    {
        currAlphaIdx += count;

        if (currAlphaIdx > 26)
            currAlphaIdx = 0;
        if (currAlphaIdx < 0)
            currAlphaIdx = 26;

        currAlpha = alphabets[currAlphaIdx];
        //Vector3 currAlphaTrans = arrowTransform;
    }

    void SelectAlpha()
    {
        if (currAlphaIdx == 26)
        {
            string origin = answText.text;
            string newText = "";

            for (int i = 0; i < origin.Length - 1; i++)
                newText += origin[i];

            Debug.Log("삭제완료!    " + newText);

            answText.text = newText;
        }
        else
            answText.text += alphabets[currAlphaIdx];

        Debug.Log(answText.text);
        Debug.Log(questions[quesIdx]);

        if (answText.text.Equals(questions[quesIdx]))
        {
            Debug.Log("correct!");
            quesCount++;

            if (quesCount > maxCount)
                GameClear();

            answText.text = "";
            SpawnQues();
        }
    }

    void SpawnQues()
    {
        quesIdx = Random.Range(0, questions.Length);
        Debug.Log(quesIdx);

        quesText.text = questions[quesIdx];
    }

    void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void GameClear()
    {
        gameclearText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
