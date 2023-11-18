using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public Transform enemyPrefab;
    public Transform spawnpoint;
    public float timeWaves = 10f;
    private float countdown = 2f;
    public static float enemyCount = 0f;

    void Update () {

        if (countdown <= 0f && enemyCount == 0) {

            GameManager.currentRound++;
            StartCoroutine(Spawn());
            countdown = timeWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator Spawn () {

        for (int i = 0; i < GameManager.currentRound; i++) {

            CreateEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateEnemy() {
        enemyCount++;
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    } 
}
