using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerMove : MonoBehaviour
{
    
    public float maxSpeed;
    public float jumpPower;
    bool isJumping = false;
    bool state = false;
    static bool door1 = false;
    static bool door2 = false;
    static bool door3 = false;
    public static int t = 0;
    static int heart = 5;
    static int stage = 0;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject exit1;
    public GameObject exit2;
    public GameObject exit3;
    public GameObject exit4;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    AudioSource audioSource;
    public AudioClip audioSuccess;
    public AudioClip audioJump;
    public AudioClip audioHurt;
    public AudioClip audioEnd;
    public AudioClip audioFall;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        gameObject.transform.position = ClearManager.playerPosition;

    }
    void Update()
    {
        
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                isJumping = true;
                PlaySound("Jump");

                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                
            }
            
        }
            
        //stop speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //direction sprite
        float x = Input.GetAxisRaw("Horizontal");
        
        if (x == -1)//왼쪽으로
        {
            spriteRenderer.flipX = true;
        }
        else if (x == 1)//오른쪽으로
        {
            spriteRenderer.flipX = false;
        }

        //animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);

        if (heart == 0)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //max speed
        if (rigid.velocity.x > maxSpeed)//right
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
            
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
            

        
    }
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        heart1.GetComponent<RawImage>().enabled = true;
        heart2.GetComponent<RawImage>().enabled = true;
        heart3.GetComponent<RawImage>().enabled = true;
        heart4.GetComponent<RawImage>().enabled = true;
        heart5.GetComponent<RawImage>().enabled = true;
        
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D collision)
    {
        HeartDestroy();

        if (collision.gameObject.tag == "Spike")//장애물에 부딪힘
        {
            
            
            if (gameObject.layer == 10)//무적 아닐때만 하트 감소
            {
                heart--;
                PlaySound("Damage");
            }
            OnDamaged(collision.transform.position);
            
        }

        if (collision.gameObject.tag == "Fall")//추락
        {
            PlaySound("Fall");
            heart--;
            respawn();
            
        }

        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "Door1")//첫번째 미니게임으로
        {
            door1 = true;
            stage++;
            Debug.Log(stage);
            exit1.SetActive(true);
            PlaySound("Success");
            Debug.Log(door1);
            Count();
            // 위치보정
            ClearManager.playerPosition = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f, transform.position.z);
            SceneManager.LoadScene("2.Mini1_ot");
        }
        if (collision.gameObject.tag == "Door2")//두번째 미니게임으로
        {
            door2 = true;
            stage++;
            Debug.Log(stage);
            exit2.SetActive(true);
            PlaySound("Success");
            Debug.Log(door2);
            Count();
            ClearManager.playerPosition = transform.position;
            ClearManager.playerPosition.x += 1.5f; // 위치 보정
            SceneManager.LoadScene("5.Jet_ot");
        }
        if (collision.gameObject.tag == "Door3")//세번째 미니게임으로
        {
            door3 = true;
            stage++;
            Debug.Log(stage);
            exit3.SetActive(true);
            PlaySound("Success");
            Count();
            ClearManager.playerPosition = transform.position;
            ClearManager.playerPosition.x += 1.5f; // 위치 보정
            SceneManager.LoadScene("6.Tetris_ot");

        }
        if (collision.gameObject.tag == "Door4")//게임 클리어
        {
            stage++;
            Debug.Log(stage);
            exit4.SetActive(true);
            Count();
            PlaySound("Success");
            Scenechange();
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;
        //맞았으니 투명하게
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //튕겨나감
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 1, ForceMode2D.Impulse);

        Invoke("offDamaged", 2);
       
    }

    void offDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    
   

    void respawn()
    {
        if (door1 == true)
        {
            
            if (door2 == true)
            {
                if (door3 == true)
                {
                    transform.position = new Vector3(49.5f, 1.74f, 0f);
                }
                else if (door3 == false)//door1,2만통과
                {
                    offDamaged();
                    transform.position = new Vector3(32.7f, 2.05f, 0f);
                }

            }
            else if (door2 == false)//door1 통과, 2는 통과 못함
            {
                Debug.Log("Door2");
                Debug.Log(door2);
                offDamaged();
                transform.position = new Vector3(12f, 3.253f, 0f);
            }
        }
        else if (door1 == false)//door1도 통과 못함
        {
            offDamaged();
            transform.position = new Vector3(-6.45f, 1.15f, 0f);
        }

    }

    void HeartDestroy()
    {
        if (heart == 4)
        {
            heart5.GetComponent<RawImage>().enabled = false;
        }
        else if (heart == 3)
        {
            heart5.GetComponent<RawImage>().enabled = false;
            heart4.GetComponent<RawImage>().enabled = false;
        }
        else if (heart == 2)
        {
            heart5.GetComponent<RawImage>().enabled = false;
            heart4.GetComponent<RawImage>().enabled = false;
            heart3.GetComponent<RawImage>().enabled = false;

        }
        else if (heart == 1)
        {
            heart5.GetComponent<RawImage>().enabled = false;
            heart4.GetComponent<RawImage>().enabled = false;
            heart3.GetComponent<RawImage>().enabled = false;
            heart2.GetComponent<RawImage>().enabled = false;

        }
        else if (heart == 0)
        {
            heart5.GetComponent<RawImage>().enabled = false;
            heart4.GetComponent<RawImage>().enabled = false;
            heart3.GetComponent<RawImage>().enabled = false;
            heart2.GetComponent<RawImage>().enabled = false;
            heart1.GetComponent<RawImage>().enabled = false;

        }
   
    }

    void Die() {

        t = 0;
        PlaySound("GameOver");
        anim.SetBool("isRun", false);
        anim.SetBool("isJump", false);
        anim.SetBool("isDie", true);
        Invoke("Scenechange", 1.3f);
        SceneManager.LoadScene("7.0Key");

    }

    void Count()
    {
        if ((ClearManager.stageClear[0] == true)&&(state==false)&&(stage==2))
        {
            t++;
            state = true;
            Debug.Log("게임1 클리어");
            Debug.Log(t);
            Debug.Log(state);
            
        }

        state = false;
        
        if (ClearManager.stageClear[1] == true && (state == false) && (stage == 3))
        {
            t++;
            state = true;
            Debug.Log("게임2 클리어");
            Debug.Log(t);
            Debug.Log(state);
        }

        state = false;

        if (ClearManager.stageClear[2] == true && (state == false) && (stage == 4))
        {
            t++;
            state = true;
            Debug.Log("게임3 클리어");
            Debug.Log(t);
            Debug.Log(state);
        }


    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "Success":
                audioSource.clip = audioSuccess;
                break;
            case "Jump":
                audioSource.clip = audioJump;
                break;
            case "Damage":
                audioSource.clip = audioHurt;
                break;
            case "GameOver":
                audioSource.clip = audioEnd;
                break;
            case "Fall":
                audioSource.clip = audioFall;
                break;
        }

        audioSource.Play();
    }
    void Scenechange() {

        switch (t)
        {
            case 0:
                SceneManager.LoadScene("8.1-2Key");
                break;
            case 1:
                SceneManager.LoadScene("8.1-2Key");
                break;
            case 2:
                SceneManager.LoadScene("8.1-2Key");
                break;
            case 3:
                SceneManager.LoadScene("9.3Key");
                break;
        }
        
    }
}
