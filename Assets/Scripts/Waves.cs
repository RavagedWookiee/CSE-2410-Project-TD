using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
    public Transform enemyPrefab;
    public Transform spawnpoint;
    public float timeWaves = 10f;
    private float countdown = 2f;

    public static int wave = 0;

    void Update () {

        if (countdown <= 0f) {

            //add check to make sure all enemies are dead
            if (wave == 100) {

                Debug.Log("You Win!!!");
            }

            wave++;
            StartCoroutine(Spawn());
            countdown = timeWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator Spawn () {

        for (int i = 0; i < wave; i++) {

            CreateEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateEnemy() {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    } 
}
