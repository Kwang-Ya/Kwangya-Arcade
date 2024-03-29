﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManager;

    public float speed;
    public GameObject PlayerBullet;
    public GameObject PlayerBullet02;

    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject Explosion;
    public GameObject scoreUIText;
    public GameObject ItemStateText;

    public AudioSource itemSound;
    public AudioSource shootingSound1;
    public AudioSource shootingSound2;

    public Text LivesUIText;
    public Text itemStateText;
    const int MaxLives = 5;
    int lives;

    int now;

    public int Attack;
    public int attackLevel;
    int speedLevel;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = "LIVES x " + lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        attackLevel = 1;
        Attack = 100;
        now = 0;
        speedLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // 클릭시 총알 발사
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet01, bullet02;

            if (attackLevel < 3)
            {
                shootingSound1.Play();
                bullet01 = (GameObject)Instantiate(PlayerBullet);
                bullet02 = (GameObject)Instantiate(PlayerBullet);
            }
            else
            {
                shootingSound2.Play();
                bullet01 = (GameObject)Instantiate(PlayerBullet02);
                bullet02 = (GameObject)Instantiate(PlayerBullet02);
            }

            bullet01.transform.position = bulletPosition01.transform.position;
            bullet01.GetComponent<PlayerBullet>().speed *= speedLevel;

            bullet02.transform.position = bulletPosition02.transform.position;
            bullet02.GetComponent<PlayerBullet>().speed *= speedLevel;
        }

        float x = Input.GetAxisRaw("Horizontal");   // -1, 0, 1(left, no input, right)
        float y = Input.GetAxisRaw("Vertical");     // -1, 0, 1(down, no input, up)

        // 벡터 정규화
        Vector2 direction = new Vector2(x, y).normalized;

        // 위치 설정
        Move(direction);

        if (scoreUIText.GetComponent<GameScore>().Score >= 6000) // 게임 클리어 조건
        {
            GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.Gameclear);
            gameObject.SetActive(false);
        }
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.33f, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.66f, 1));

        max.x = max.x - 0.225f; // player sprite half width .....어라
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        int passedTime = scoreUIText.GetComponent<GameScore>().Score / 1000;
        if (now < passedTime)
        {
            now = passedTime;
            speed += 0.5f; // 점수 증가할 때 마다 속도 증가
        }

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--; // 생명 감소
            if (lives <= 1)
            {
                LivesUIText.text = "LIFE x " + lives.ToString();
            }
            else if (lives > 1)
            {
                LivesUIText.text = "LIVES x " + lives.ToString();
            }


            if (lives == 0) // 게임 종료
            {
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.Gameover);
                gameObject.SetActive(false);
            }
        }

        else if (collision.tag == "ItemTag")
        {
            float percent = Random.Range(0f, 10f);
            itemSound.Play();

            if (percent < 5f) // 50%의 확률로 공격속도증가
            {
                IncreaseAttackSpeed();
            }
            else             // 50%의 확률로 공격력 증가
            {
                IncreaseAttackLevel();
            }
            Destroy(collision.gameObject);
        }
    }

    void IncreaseAttackSpeed()
    {
        ItemStateText.SetActive(true);
        itemStateText.text = "공격 속도 증가";

        Invoke("DestroyText", 1.5f);
        if (speedLevel < 5) // 속도제한
        {
            speedLevel += 1;
        }
    }

    void IncreaseAttackLevel()
    {
        ItemStateText.SetActive(true);
        itemStateText.text = "공격력 증가";

        Invoke("DestroyText", 1.5f);
        if (attackLevel < 6) // 공격력제한
        {
            attackLevel += 1;
            Attack += 100;
            Debug.Log(Attack);
        }
    }

    void DestroyText()
    {
        ItemStateText.SetActive(false);
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
