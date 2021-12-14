﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner4 : MonoBehaviour
{
    public GameObject scoreUIText;
    public GameObject Enemy;

    float maxSpawnRateInSeconds = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        if (scoreUIText.GetComponent<GameScore>().Score > 4000)
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0.33f, 0));

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.66f, 1));

            GameObject anEnemy = (GameObject)Instantiate(Enemy);
            anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInNSeconds;

        if (maxSpawnRateInSeconds > 2f)
        {
            spawnInNSeconds = Random.Range(2f, maxSpawnRateInSeconds);
        }
        else spawnInNSeconds = 2f;

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 2f) maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 2f) CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleEnemySpawner()
    {
        maxSpawnRateInSeconds = 10f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}