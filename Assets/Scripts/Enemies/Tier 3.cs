using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3Enemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 20f;
        health = maxHealth;

        lifeDamage = 3;

        speed = 10f;
        originalSpeed = speed;
        reward = 30;

        base.Start();
    }

}
