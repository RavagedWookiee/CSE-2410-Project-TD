using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{   
    private Transform target;

    [Header("Attributes")]

    public float range = 10f;
    public float fireRate = 1f;
    private float fireCountDown = 1f;
    public float damage = 5f;

    [Header("Unity Side")]

    public string enemyTag = "Enemy";
    public float turnspeed = 10f;
    public Transform top;

    public GameObject bulletPrefab;
    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TargetUpdate", 0f, 0.5f);   
    }

    void TargetUpdate() {

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
    void Update()
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

    void Shoot () { 

        GameObject bulletTemp = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletTemp.GetComponent<Bullet>();
        bullet.damage = damage;

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected () {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
