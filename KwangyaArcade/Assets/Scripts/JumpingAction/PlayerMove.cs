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
    bool checkdead = false;
    bool door1 = false;
    bool door2 = false;
    bool door3 = false;
    public static int heart = 5;
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
    

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        
        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                isJumping = true;
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
            checkdead = true;
            Die();
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //max speed
        if (rigid.velocity.x > maxSpeed)//right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))//left
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        
    }
    void Start()
    {
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

            if (heart == 5)
            {
                heart--;
            }
            if (gameObject.layer == 10)//무적 아닐때만 하트 감소
            {
                heart--;
            }
            OnDamaged(collision.transform.position);
            
        }

        if (collision.gameObject.tag == "Fall")//추락
        {
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
            exit1.SetActive(true);
            Debug.Log(door1);
            SceneManager.LoadScene("Bisibasi_1");
        }
        if (collision.gameObject.tag == "Door2")//두번째 미니게임으로
        {
            door2 = true;
            exit2.SetActive(true);
            Debug.Log(door2);
            SceneManager.LoadScene("JetGame");
        }
        if (collision.gameObject.tag == "Door3")//세번째 미니게임으로
        {
            exit3.SetActive(true);
            SceneManager.LoadScene("Tetris");

        }
        if (collision.gameObject.tag == "Door4")//게임 클리어
        {
            exit4.SetActive(true);
            SceneManager.LoadScene("Ending");
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
                if (door3 == true) {
                    transform.position = new Vector3(49.5f, 1.74f, 0f);
                }
                else if (door3 == false)//door1,2만통과
                {
                    offDamaged();
                    transform.position = new Vector3(32.7f, 2.05f,0f);
                }
                
            }
            else//door1 통과, 2는 통과 못함
            {
                offDamaged();
                transform.position = new Vector3(12f,3.253f, 0f);
            }
        }
        else//door1도 통과 못함
        {
            offDamaged();
            transform.position = new Vector3(-6.45f,1.15f, 0f);
        }

    }

    void HeartDestroy()
    {
        switch (heart)
        {
            case 4:
                heart5.GetComponent<RawImage>().enabled = false;
                break;
            case 3:
                heart4.GetComponent<RawImage>().enabled = false;
                break;
            case 2:
                heart3.GetComponent<RawImage>().enabled = false;
                break;
            case 1:
                heart2.GetComponent<RawImage>().enabled = false;
                break;
            case 0:
                heart1.GetComponent<RawImage>().enabled = false;
                break;
        }
    }

    void Die() {

        if (checkdead) {
            anim.SetBool("isRun", false);
            anim.SetBool("isJump", false);
            anim.SetBool("isDie", true);
            checkdead = false;
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isJump", false);
            anim.SetBool("isDie", false);
        }
        SceneManager.LoadScene("Dead_JumpingAction");

    }
}
