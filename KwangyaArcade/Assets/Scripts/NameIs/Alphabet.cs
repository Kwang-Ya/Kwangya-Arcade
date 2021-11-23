using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    public char[] alphabetArr;
    public Text alphabetText;

    void Start()
    {
        InitializeArr();
    }

    public void InitializeArr()
    {
        alphabetArr = new char[27];
        alphabetText.text = "";

        for (int i = 0; i < 27; i++)
        {
            if (i == 26)
                alphabetArr[i] = '>';
            else
                alphabetArr[i] = (char)(65 + i);

            alphabetText.text += alphabetArr[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
