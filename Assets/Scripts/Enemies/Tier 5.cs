using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T5Enemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 80f;
        health = maxHealth;

        lifeDamage = 5;

        speed = 10f;
        originalSpeed = speed;
        reward = 50;

        base.Start();
    }

}
