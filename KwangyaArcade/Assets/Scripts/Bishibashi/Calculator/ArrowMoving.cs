using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoving : MonoBehaviour
{
    const float offset = 1.45f;

    const float initialX = -1.45f;
    const float initialY = -0.49f;

    public AudioSource moveSound;
    public int moveCount;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector2(initialX, initialY);
        moveCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("left"))
        {
            moveCount--;
            moveSound.Play();
        }
        else if(Input.GetKeyDown("right"))
        {
            moveCount++;
            moveSound.Play();
        }

        if (moveCount < 0) moveCount = 11;

        else if (moveCount > 11) moveCount = 0;

        Vector2 transform = new Vector2(initialX + offset * (moveCount % 3), initialY - offset * (moveCount / 3));
        gameObject.transform.position = transform;
    }
}
