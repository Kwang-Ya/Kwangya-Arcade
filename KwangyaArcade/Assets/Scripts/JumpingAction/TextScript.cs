using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text ClearText;
    public Text GameOver;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

        //if(GameObject.Find("clear").GetComponent<PlayerMove>().clear== true)
        {
            ClearText.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(false);
            ClearText.text = "수많은 노력 끝에 결국 오락실 주인의 기록을 깬 당신! 축하합니다!";

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
