using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2Enemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 10f;
        health = maxHealth;

        lifeDamage = 2;

        speed = 10f;
        originalSpeed = speed;
        reward = 20;

        base.Start();
    }

}
