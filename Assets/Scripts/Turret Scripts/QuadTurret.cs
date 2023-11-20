using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTurret : Tower
{   
    [Header("Quad Attributes")]
    private float fasterFire = 2f;
    public float dam = 0.5f;

    [Header("Unity Side")]

    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();  
        damage = dam;
        fireRate = fasterFire;
    }

    new void TargetUpdate() {

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
    new void Update()
    {
        if (target == null) {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(top.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        top.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f) {

            Shoot1();
            fireCountDown = 1f / fireRate;
        }

        if (fireCountDown <= 0f) {

            Shoot2();
            fasterFire = 1f / fireRate + 0.5f;
        }

        fireCountDown -= Time.deltaTime;

    }

    void Shoot1 () { 

        GameObject bulletTemp1 = (GameObject)Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Bullet bullet1 = bulletTemp1.GetComponent<Bullet>();
        bullet1.damage = damage;

        GameObject bulletTemp2 = (GameObject)Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Bullet bullet2 = bulletTemp2.GetComponent<Bullet>();
        bullet1.damage = damage;

        if (bullet1 != null && bullet2 != null) {
            bullet1.Seek(target);
            bullet2.Seek(target);
        }
    }

    void Shoot2 () { 

        GameObject bulletTemp1 = (GameObject)Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Bullet bullet1 = bulletTemp1.GetComponent<Bullet>();
        bullet1.damage = damage;

        GameObject bulletTemp2 = (GameObject)Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
        Bullet bullet2 = bulletTemp2.GetComponent<Bullet>();
        bullet1.damage = damage;

        if (bullet1 != null && bullet2 != null) {
            bullet1.Seek(target);
            bullet2.Seek(target);
        }
    }

    new void OnDrawGizmosSelected () {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
