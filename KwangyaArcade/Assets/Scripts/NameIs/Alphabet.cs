using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    public char[] alphabets;
    public Vector3[] alphaTransform;


    public Text alphabetText;

    //public Vector3 arrowTransform = new Vector3(-575.2f, -322.5f, 0f);
    public Transform arrow;
    public Vector3 arrowTransform;

    public Vector3 xOffset = new Vector3(0.21f, 0f, 0f);

    void Start()
    {
        arrowTransform = arrow.gameObject.transform.position;

        Initialize();

    }

    public void Initialize()
    {
        alphabets = new char[27];
        alphabetText.text = "";

        for (int i = 0; i < alphabets.Length; i++)
        {
            if (i == 26)
                alphabets[i] = '>';
            else
                alphabets[i] = (char)(65 + i);

            alphabetText.text += alphabets[i];
        }

        for (int i = 0; i < alphabets.Length; i++)
        {
            alphaTransform[i] = calcOffset(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("left"))   // 빨 -> 1 UP
        {
            Move(calcOffset(1));
        }
        if (Input.GetKeyDown("down"))   // 초 -> 2 UP
        {
            Move(calcOffset(2));
        }
        if (Input.GetKeyDown("right"))   // 파 -> 3 UP
        {
            Move(calcOffset(3));
        }

        arrow.gameObject.transform.position = arrowTransform;
    }

    private bool Move(Vector3 translation)
    {
        Vector3 newPosition = arrow.gameObject.transform.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        // 위치 이동할 수 있는지 조건 확인

        // arrow를 새 위치로
        arrowTransform = newPosition;

        return true;
    }

    Vector3 calcOffset(int count)
    {
        Vector3 vec = arrowTransform + xOffset * count;

        return vec;
    }
}
