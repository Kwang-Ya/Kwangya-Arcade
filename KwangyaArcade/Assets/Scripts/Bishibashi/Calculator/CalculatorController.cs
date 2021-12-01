using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorController : MonoBehaviour
{
    public ArrowMoving arrow;
    public Text numberText;

    string answer = "";
    string[] calculator =
    {
        "7", "8", "9",
        "4", "5", "6",
        "1", "2", "3",
        "equal", ".", "0"
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("down"))
        {
            if(arrow.moveCount == 9)
            {
                answer = "";
            }
            else
            {
                answer += calculator[arrow.moveCount];
            }

            numberText.text = answer;
        }
    }
}
