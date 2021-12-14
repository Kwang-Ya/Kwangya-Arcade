using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_three : MonoBehaviour
{
    int count = 0;
    public Text myText;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
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

   

    void ShowText() {
        if (count == 0)
        {
            myText.text = "이런... 당신이 이 모든 게임을 클리어할거라\n생각하진 못했는데..기대 이상인걸?";
        }
        else if (count == 1)
        {
            myText.text = "여기 광야 아케이드에서 탈출하게 된걸 진심으로\n축하하네! 이제 이 넓은 광야에서 벗어나게나!";
            Destroy(button);
            Destroy(buttontext);
        }
        
    }

    IEnumerator ShowKey() {

        if (count == 1)
        {
            yield return new WaitForSeconds(1f);
            key1.SetActive(true);
            yield return new WaitForSeconds(1f);
            key2.SetActive(true);
            yield return new WaitForSeconds(1f);
            key3.SetActive(true);

        }
    }
}
