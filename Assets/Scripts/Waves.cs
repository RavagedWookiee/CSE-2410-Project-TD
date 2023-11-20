using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public Transform spawnpoint;
    public float timeWaves = 10f;
    private float countdown = 2f;
    public static float enemyCount = 0f;
    public EnemyTier[] enemyTiers;
    public float increaseFactor = 1.1f; // Factor to increase enemy count each round
    private int enemiesToSpawn;

    void Update () {

        if (countdown <= 0f && enemyCount == 0) {

            GameManager.currentRound++;
            enemiesToSpawn = Mathf.RoundToInt(30 * Mathf.Pow(increaseFactor, GameManager.currentRound - 1));
            StartCoroutine(Spawn());
            countdown = timeWaves;
        }



        countdown -= Time.deltaTime;
    }

    IEnumerator Spawn () {

        for (int i = 0; i < enemiesToSpawn; i++) {

            CreateEnemy(GameManager.currentRound);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateEnemy(int round) {
        enemyCount++;

        int totalWeight = 0;
        foreach (var tier in enemyTiers)
        {
            totalWeight += Mathf.RoundToInt(Mathf.Lerp(tier.initialWeight, tier.finalWeight, (float)round / 100));
        }

        int pick = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        foreach (var tier in enemyTiers)
        {
            cumulativeWeight += Mathf.RoundToInt(Mathf.Lerp(tier.initialWeight, tier.finalWeight, (float)round / 100));
            if (pick <= cumulativeWeight)
            {
                Instantiate(tier.prefab, spawnpoint.position, spawnpoint.rotation);
                return;
            }
        }
    } 
}

[System.Serializable]
public class EnemyTier
{
    public Transform prefab;
    public int initialWeight;
    public int finalWeight;
}
