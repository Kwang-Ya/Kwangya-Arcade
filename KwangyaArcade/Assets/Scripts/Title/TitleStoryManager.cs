using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleStoryManager : MonoBehaviour
{
    int clickCounter = 0;
    public GameObject Dialog;
    public GameObject LetterClosed;
    public GameObject LetterOpened;

    public GameObject Papyrus;

    public Text DialogText;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (clickCounter) {
            case 0:
                DialogText.text = "어라...여긴 어디지? ";
                break;
            case 1:
                DialogText.text = "난 분명 오락실에 들어왔는데..? ";
                break;
            case 2:
                DialogText.text = "어.. 일단 저 쪽지를 볼까? ";
                break;
            case 3:
                LetterClosed.SetActive(false);
                DialogText.text = "<color=#6F6F6F>" + "(쪽지의 내용을 확인해보자)" + "</color>";
                LetterOpened.SetActive(true);
                break;
            case 4:
                LetterOpened.SetActive(false);
                Papyrus.SetActive(true);
                DialogText.text = "안녕 나는 광-야 아케이드의 주인장이야!\n많이 당황했겠지만 여길 나가려면 오락실 게임으로 나를 이겨야 한단다!";
                break;
            case 5:
                DialogText.text = "듣자하니 엄청난 실력을 가졌다하는데..\n나를 이길 수 있을지 한번 기대해볼게!";
                break;
            case 6:
                DialogText.text = "행운을 빈다! 물론 어렵겠지만.";
                break;
            default:
                Invoke("SceneMove", 1f);
                Dialog.SetActive(false);
                DialogText.text = "";
                break;
        }
    }

    public void ClickOnCount()
    {
        clickCounter++;
    }

    public void SceneMove()
    {
        SceneManager.LoadScene("JumpingAction");
    }
}
