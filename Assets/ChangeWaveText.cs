using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeWaveText : MonoBehaviour
{
    public int currentWave;
    public TMPro.TMP_Text currentWaveText;
    public TMPro.TMP_Text nextWaveText;
    WaveSpawner waveSpawner;

    void Start()
    {
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        currentWave = waveSpawner.currentWave;
        currentWaveText.text = string.Format("Wave: {0}", currentWave);
        if (waveSpawner.state == WaveSpawner.SpawnState.COUNTING) {
            nextWaveText.text = string.Format("Next wave in: {0}s", Math.Floor(waveSpawner.waveCountdown));
        } else {
            nextWaveText.text = "";
        }
    }
}
