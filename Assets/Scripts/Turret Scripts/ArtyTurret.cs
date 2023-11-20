using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyTurret : Tower
{   
    [Header("Arty Attributes")]

    public float minRange = 15f;
    public float maxRange = 100f;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        range = maxRange;
        damage = 50f;
        fireRate = 0.25f; 
    }

    new void TargetUpdate() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies) {

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy > minRange) {

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

    new void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(top.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
        top.rotation = Quaternion.Euler(0f, rotation.y, 0f);

         Debug.DrawLine(top.position, target.position, Color.red); // Draw a red line
        

        if (fireCountDown <= 0f)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > minRange)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
        }

       fireCountDown -= Time.deltaTime;
    }

    protected override void Shoot () { 

        if (target != null)  // Check if the target still exists
        {
            GameObject bulletTemp = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletTemp.GetComponent<Bullet>();
            bullet.damage = damage;

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        
        }
    }

    new void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
}
