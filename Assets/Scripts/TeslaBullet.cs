using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 60f;
    public float slowEffectStrength = 0.5f; // Slow down to 50% speed
    public float slowDuration = 2f; // Slow effect lasts for 2 seconds
    public GameObject impactEffect;

    public void Seek (Transform _target) {

        target = _target;
    }

    // Update is called once per frame
    void Update() {
        
        if (target == null) {

            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) {

            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget () {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 0.5f);
        
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {   
            enemy.ApplySlow(slowEffectStrength, slowDuration);
        }
        
        Destroy(gameObject);
    }
}
