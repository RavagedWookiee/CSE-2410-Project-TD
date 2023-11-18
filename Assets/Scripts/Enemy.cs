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
    public float health;
    public float maxHealth;
    public int reward;

    protected virtual void Start() {

        target = Waypoint.points[0];
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
            PlayerStats.Lives -= 1;
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
