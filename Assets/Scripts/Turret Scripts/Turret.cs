using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{   
    public Transform target;

    [Header("Energy Attributes")]
    public float maxEnergy = 100f; // Maximum energy capacity
    public float currentEnergy; // Current energy level
    public float energyConsumptionRate = 1f; // Energy consumed per shot

    [Header("Upgrades")]

    public bool rangeUp = false;
    public bool fireRateUp = false;
    public bool damageUp = false;

    [Header("Attributes")]

    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountDown = 0f;
    public float damage = 5f;

    [Header("Unity Side")]

    public string enemyTag = "Enemy";
    public float turnspeed = 10f;
    public Transform top;

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.5f);   
    }

    protected virtual void TargetUpdate() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies) {

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {

                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {

            target = nearestEnemy.transform;
        }
        else {

            target = null;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target == null) {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(top.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        top.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f) {

            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;

    }

    protected virtual void Shoot () { 

        GameObject bulletTemp = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletTemp.GetComponent<Bullet>();
        bullet.damage = damage;

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    protected virtual void OnDrawGizmosSelected () {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
