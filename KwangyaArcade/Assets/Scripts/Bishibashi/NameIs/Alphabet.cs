using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    public char[] alphabets;
    public Vector3[] alphaTransform;


    public Text alphabetText;

    public Transform arrow;
    public Vector3 arrowTransform;
    public Vector3 leftmostArrowTransform;      // A 칸 위치
    public Vector3 rightmostArrowTransform;      // A 칸 위치

    public Vector3 xOffset;

    void Start()
    {
        xOffset = new Vector3(0.263f, 0f, 0f);
        arrowTransform = arrow.gameObject.transform.position;
        leftmostArrowTransform = arrowTransform;
        rightmostArrowTransform = arrowTransform + calcOffset(26);

        Debug.Log(arrowTransform);
        Initialize();
    }

    public void Initialize()
    {
        alphabets = new char[27];
        alphaTransform = new Vector3[alphabets.Length];
        alphabetText.text = "";

        for (int i = 0; i < alphabets.Length; i++)
        {
            if (i == 26)
                alphabets[i] = '<';
            else
                alphabets[i] = (char)(65 + i);

            alphabetText.text += alphabets[i];
            alphaTransform[i] = new Vector3(0f, 0f, 0f) + calcOffset(i);

            Debug.Log(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("left"))   // 빨 -> 왼쪽 이동
        {
            Debug.Log("left");
            Move(calcOffset(-1));
        }
        if (Input.GetKeyDown("down"))   // 초 -> 선택
        {
            Debug.Log("select");
            // 선택
        }
        if (Input.GetKeyDown("right"))   // 파 -> 오른쪽 이동
        {
            Debug.Log("right");
            Move(calcOffset(1));
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
            newPosition.x = leftmostArrowTransform.x;
        // 맨 왼쪽 A 위치에서 왼쪽으로 이동하면 맨 오른쪽 < 위치로
        else if (newPosition.x < leftmostArrowTransform.x)
            newPosition.x = rightmostArrowTransform.x;

        // arrow를 새 위치로
        arrowTransform = newPosition;

        return true;
    }

    Vector3 calcOffset(int count)
    {
        Vector3 vec = xOffset * count;
        
        return vec;
    }
}
