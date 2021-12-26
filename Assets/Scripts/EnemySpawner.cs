using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    private int startingWave = 0;
    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount <= currentWave.GetNumberOfEnemies(); enemyCount++)
        {
        Instantiate(
            currentWave.GetEnemyPrefab(), 
            currentWave.GetWayPoints()[enemyCount].transform.position, 
            Quaternion.identity);
        yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
