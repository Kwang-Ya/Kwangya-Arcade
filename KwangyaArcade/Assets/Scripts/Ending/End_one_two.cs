using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_one_two : MonoBehaviour
{
    int count = 0;
    bool onekey = false;
    bool twokey = false;
    public Text myText;
    public GameObject key1;
    public GameObject key2;
    public GameObject button;
    public GameObject buttontext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            count++;
        }
        ShowText();
        StartCoroutine("ShowKey");
    }


    


    void ShowText()
    {
        if (PlayerMove.t == 0) {

            switch (count)
            {
                case 0:
                    myText.text = "이런 이런.. 단 하나의 스테이지도\n클리어하지 못했다니 좀 실망인걸?";
                    break;
                case 1:
                    myText.text = "하지만 이 마지막 문까지 온 것만으로 박수를 쳐주지";
                    break;
                case 2:
                    myText.text = "광야를 탈출하기 위해 끝없이 도전하길 바라!";
                    Destroy(button);
                    Destroy(buttontext);
                    break;
            }
        }
        if (PlayerMove.t == 1)
        {
            onekey = true;

            switch (count)
            {
                case 0:
                    myText.text = "모든 스테이지를 클리어하진\n못했지만 좋은 플레이었어!";
                    break;
                case 1:
                    myText.text = "비록 열쇠의 개수는 조금 모자라지만\n즐거운 시간이었네 자네";
                    break;
                case 2:
                    myText.text = "하지만 자네는 아직 이 광야를 탈출하지\n못했단 사실을 기억하라고!";
                    Destroy(button);
                    Destroy(buttontext);
                    break;
            }


        }
        else if (PlayerMove.t == 2)
        {
            twokey = true;
            switch (count)
            {

                case 0:
                    myText.text = "모든 스테이지를 클리어하진\n못했지만 좋은 플레이었어!";
                    break;
                case 1:
                    myText.text = "열쇠를 하나 더 모았으면 완벽했을텐데 아쉽군";
                    break;
                case 2:
                    myText.text = "자네, 게임 다시 해볼 생각 없나?";
                    break;
                case 3:
                    myText.text = "하하하 장난일세!";
                    break;
                case 4:
                    myText.text = "하지만 자네는 아직 이 광야를 탈출하지\n못했단 사실을 기억하라고!";
                    Destroy(button);

                    break;
            }

        }
        
    }
        

    

    IEnumerator ShowKey()
    {

        if (onekey==true && twokey==false)
        {
            yield return new WaitForSeconds(1f);
            key1.SetActive(true);
            

        }
        else if (onekey == false && twokey == true)
        {
            yield return new WaitForSeconds(1f);
            key1.SetActive(true);
            yield return new WaitForSeconds(1f);
            key2.SetActive(true);


        }
    }
}
