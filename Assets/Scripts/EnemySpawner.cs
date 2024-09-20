using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    
    private float spawnNext = 0f;
    private PoolManager meteorPoolManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the second pool manager
        meteorPoolManager = GameObject.FindGameObjectWithTag("Pool").GetComponents<PoolManager>()[1];
    }
    
    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManager.isPaused)
            return;

        if (Time.time > spawnNext) {
            spawnNext = Time.time + 60f / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            
            float rand = Random.Range(-xLimit, xLimit);
            Vector2 spawnPosition = new Vector2(rand, 8f);
            meteorPoolManager.GetObject(spawnPosition);
        }
    }
}
