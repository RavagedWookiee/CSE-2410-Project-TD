using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy: Enemy
{
    protected override void Start()
    {
        maxHealth = 5f;
        health = maxHealth;

        speed = 10f;
        reward = 10;

        base.Start();
    }

}
