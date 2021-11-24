using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerControl : MonoBehaviour
{
    public BlockGenerator blockGenerator;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(count < blockGenerator.maxBlock)
        {
            if(Input.GetKeyDown("z"))
            {
                if(blockGenerator.blockArray[count].tag == "RedTag")
                {
                    Destroy(blockGenerator.blockArray[count++].gameObject);
                }
            }
            if (Input.GetKeyDown("x"))
            {
                if (blockGenerator.blockArray[count].tag == "GreenTag")
                {
                    Destroy(blockGenerator.blockArray[count++].gameObject);
                }
            }
            if (Input.GetKeyDown("c"))
            {
                if (blockGenerator.blockArray[count].tag == "BlueTag")
                {
                    Destroy(blockGenerator.blockArray[count++].gameObject);
                }
            }
        }
        //if (Input.GetKeyDown("z"))
        //{
        //    if (count < blockGenerator.maxBlock)
        //    {
        //        Destroy(blockGenerator.blockArray[count].gameObject);
        //        count++;
        //    }
        //}
    }
}
