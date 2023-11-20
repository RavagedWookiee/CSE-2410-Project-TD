using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    public GameObject impactEffect;
    
    [Header("Attributes")]
    public float speed;
    public float originalSpeed;
    private bool isSlowed = false;
    public float health;
    public float maxHealth;
    public int reward;
    public int lifeDamage;

    protected virtual void Start() {

        target = Waypoint.points[0];
    }

    public void ApplySlow(float slowStrength, float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(Slow(slowStrength, duration));
            Debug.Log("slowed");
        }
        else
        {
            // Reset the slow effect if already slowed
            StopCoroutine("Slow");
            StartCoroutine(Slow(slowStrength, duration));
        }
    }

    IEnumerator Slow(float slowStrength, float duration)
    {
        isSlowed = true;
        speed *= slowStrength;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        isSlowed = false;
    }


    protected virtual void Update () {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {

            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {

        if(waypointIndex >= Waypoint.points.Length - 1) {

            Destroy(gameObject);
            PlayerStats.Lives -= lifeDamage;
            return;
        }

        waypointIndex++;
        target = Waypoint.points[waypointIndex];
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        PlayerStats.Currency += reward;
        WaveSpawner.enemyCount--;
        Destroy(gameObject);
    }
}
