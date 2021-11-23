using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blockArray;

    public GameObject RedBlock;
    public GameObject GreenBlock;
    public GameObject BlueBlock;

    public int maxBlock = 40;

    float initialY = -4.438f;
    float gap = 1.072f;
    //int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        blockArray = new GameObject[maxBlock];

        for (int i = 0; i < maxBlock; i++)
        {
            float percent = Random.Range(0f, 3f);
            if (percent < 1f)
            {
                blockArray[i] = (GameObject)Instantiate(RedBlock);
            }
            else if (percent < 2f)
            {
                blockArray[i] = (GameObject)Instantiate(GreenBlock);
            }
            else
            {
                blockArray[i] = (GameObject)Instantiate(BlueBlock);
            }

            blockArray[i].transform.position = new Vector2(0, initialY + gap * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 얘는 다른 playerController 코드에 이식할것
        //if(Input.GetKeyDown("space"))
        //{
        //    if(count < maxBlock)
        //    {
        //        Destroy(blockArray[count].gameObject);
        //        count++;
        //    }
        //}
    }
}
