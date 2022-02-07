using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }
    [System.Serializable]
    public class SpecialWave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public SpecialWave[] specialWaves;
    private int nextWave = 0;

    public int currentWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public FirstAidKitSpawner FirstAidKitSpawner;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state  == SpawnState.WAITING) {
            if (! EnemyIsAlive()) {
                WaveCompleted();
            } else {
                return;
            }
        }

        if  (waveCountdown <= 0f) {
            if (state != SpawnState.SPAWNING) {
                currentWave++;
                StartCoroutine(SpawnWave(waves[nextWave]));
                StartCoroutine(SpawnSpecialWave(specialWaves[nextWave]));
            }
        } else {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) {
            nextWave = 0;

            Debug.Log("GG");
        } else {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        FirstAidKitSpawner.SpawnFirstAidKit();
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++) {
            foreach (Transform enemy in _wave.enemies) {
                SpawnEnemy(enemy);
            }
            
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        _wave.count = _wave.count + 2;

        state = SpawnState.WAITING;

        yield break;
    }

        IEnumerator SpawnSpecialWave(SpecialWave _wave)
    {
        for (int i = 0; i < _wave.count; i++) {
            foreach (Transform enemy in _wave.enemies) {
                SpawnEnemy(enemy);
            }
            
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
