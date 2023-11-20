using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4Enemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 40f;
        health = maxHealth;

        lifeDamage = 4;

        speed = 10f;
        originalSpeed = speed;
        reward = 40;

        base.Start();
    }

}
