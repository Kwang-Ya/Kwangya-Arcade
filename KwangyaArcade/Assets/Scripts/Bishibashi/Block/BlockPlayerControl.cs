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
                if (Input.GetKeyDown("z"))
                {
                    if (blockGenerator.blockArray[count].tag == "RedTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                    }
                    else time -= 5f;
                }
                if (Input.GetKeyDown("x"))
                {
                    if (blockGenerator.blockArray[count].tag == "GreenTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                    }
                    else time -= 5f;
                }
                if (Input.GetKeyDown("c"))
                {
                    if (blockGenerator.blockArray[count].tag == "BlueTag")
                    {
                        Destroy(blockGenerator.blockArray[count++].gameObject);
                    }
                    else time -= 5f;
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
