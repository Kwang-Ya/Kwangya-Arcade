using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPlayerControl : MonoBehaviour
{
    public BlockGenerator blockGenerator;

    public GameObject gameOverText;
    public GameObject gameClearText;
    public GameObject timerText;

    public AudioSource rightSound;
    public AudioSource wrongSound;
    public AudioSource clearSound;

    public Text TimerText;
    private float time = 60f;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;

            if (count < blockGenerator.maxBlock)
            {
                if (Input.GetKeyDown("left"))
                {
                    if (blockGenerator.blockArray[count].tag == "RedTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                        rightSound.Play();
                    }
                    else
                    {
                        time -= 5f;
                        wrongSound.Play();
                    }
                }
                if (Input.GetKeyDown("down"))
                {
                    if (blockGenerator.blockArray[count].tag == "GreenTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                        rightSound.Play();
                    }
                    else
                    {
                        time -= 5f;
                        wrongSound.Play();
                    }
                }
                if (Input.GetKeyDown("right"))
                {
                    if (blockGenerator.blockArray[count].tag == "BlueTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                        rightSound.Play();
                    }
                    else
                    {
                        time -= 5f;
                        wrongSound.Play();
                    }
                }
            }

            if(count == blockGenerator.maxBlock)
            {
                timerText.SetActive(false);
                gameClearText.SetActive(true);
            }
        }
        else
        {
            time = 0f;
            gameOverText.SetActive(true);
        }

        TimerText.text = string.Format("{0:N2}", time);
    }
}
