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

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;

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

        if(transform.position.y < min.y)
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
