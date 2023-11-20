using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyBullet : MonoBehaviour {

    private Transform target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public GameObject impactEffect;
    public float damage;
    public float explosionRadius = 5f;

    private Transform myTransform;
    void Awake()
    {
        myTransform = transform; 
    }

    public void Seek (Transform _target) {

        target = _target;
        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before firing
        yield return new WaitForSeconds(0.1f);

        // Calculate distance to target
        float targetDistance = Vector3.Distance(myTransform.position, target.position);
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        float projectileVelocity = Mathf.Sqrt(targetDistance * gravity / Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad));
        projectileVelocity *= 50f;
        
        float timeToHit = targetDistance / projectileVelocity;
        Vector3 futurePosition = target.position + targetVelocity * timeToHit;

        

        // Extract the X & Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time
        float flightDuration = targetDistance / Vx;

        // Rotate projectile to face the target
        myTransform.rotation = Quaternion.LookRotation(target.position - myTransform.position);
        
        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            if (target == null) break;

            myTransform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        if (target != null)
        {
            Explode(); // Only explode if the target is still valid
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject);  // Destroy the bullet after the explosion
    }

    void Explode()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);  // Destroy the effect after 2 seconds

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Apply damage to each enemy within the radius
            }
        }
    }
}
