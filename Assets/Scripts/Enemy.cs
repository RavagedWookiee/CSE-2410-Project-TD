using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int waypointIndex = 0;

    void Start() {

        target = Waypoint.points[0];
    }

    void Update () {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {

            GetNextWaypoint();
        }
    }

    void GetNextWaypoint() {

        if(waypointIndex >= Waypoint.points.Length - 1) {

            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoint.points[waypointIndex];
    }
}
