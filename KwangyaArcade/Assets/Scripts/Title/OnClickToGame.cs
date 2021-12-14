using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickToGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Invoke("SceneMove", 1.0f);
        }
    }

    public void SceneMove()
    {
        SceneManager.LoadScene("1.title_story");
    }
}
