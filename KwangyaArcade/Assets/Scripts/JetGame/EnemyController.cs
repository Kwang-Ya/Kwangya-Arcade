using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject scoreUIText;

    GameObject player;

    float speed;
    public int health;
    public GameObject Explosion;

    int now;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        now = 0;

        player = GameObject.Find("Player");
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        int passedTime = scoreUIText.GetComponent<GameScore>().Score / 1000;
        if (now < passedTime)
        {
            now = passedTime;
            speed += 0.25f; // 점수 증가할 때 마다 속도 증가
        }

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBulletTag")
        {
            PlayExplosion();
            health -= player.GetComponent<PlayerControl>().Attack;
            scoreUIText.GetComponent<GameScore>().Score += 100;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
