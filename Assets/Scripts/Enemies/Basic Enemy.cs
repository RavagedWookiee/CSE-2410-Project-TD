using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 5f;
        health = maxHealth;

        lifeDamage = 1;

        speed = 10f;
        originalSpeed = speed;
        reward = 10;

        base.Start();
    }

}
