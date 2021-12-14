using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private MeshRenderer render;
    public GameObject scoreUIText;

    public float speed;
    private float offset;

    int now;

    // Start is called before the first frame update
    void Start()
    {
        now = 0;
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(0, offset);

        int passedTime = scoreUIText.GetComponent<GameScore>().Score / 1000;
        if(now < passedTime)
        {
            now = passedTime;
            speed += 0.05f; // 점수 증가할 때 마다 속도 증가
        }
    }
}
