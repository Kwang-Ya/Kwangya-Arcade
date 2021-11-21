using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBullet", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("Player");

        if(playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            float angle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            bullet.GetComponent<EnemyBullet>().transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
