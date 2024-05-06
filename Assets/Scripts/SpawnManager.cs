using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;

    GameObject player;
    float initialCall = 0.5f;
    float intervalCall = 2.5f;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnEnemies", initialCall, intervalCall);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            timer -= 10f;
            intervalCall -= 0.5f;
            CancelInvoke("SpawnEnemies");
            InvokeRepeating("SpawnEnemies", initialCall, intervalCall);
        }
    }

    void SpawnEnemies()
    {
        if (player != null)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
        }
    }
}
