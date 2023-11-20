using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Tower
{   
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
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
            fireCountDown -= Time.deltaTime;
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

    new void Shoot () { 

        GameObject bulletTemp = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletTemp.GetComponent<Bullet>();
        bullet.damage = damage;

        if (bullet != null) {
            bullet.Seek(target);
        }
    }

    new void OnDrawGizmosSelected () {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
