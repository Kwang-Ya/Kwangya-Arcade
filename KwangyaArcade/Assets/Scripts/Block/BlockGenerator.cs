using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blockArray;

    public GameObject RedBlock;
    public GameObject GreenBlock;
    public GameObject BlueBlock;

    public int maxBlock = 10;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxBlock; i++)
        {
            float percent = Random.Range(0f, 3f);
            if(percent < 1f)
            {
                blockArray[i] = (GameObject)Instantiate(RedBlock);
            } else if(percent < 2f)
            {
                blockArray[i] = (GameObject)Instantiate(GreenBlock);
            } else
            {
                blockArray[i] = (GameObject)Instantiate(BlueBlock);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
