using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AbstractEnemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public void heal()
    {
        health += 0.5f;
    }

    public bool isFullHealth()
    {
        return health == maxHealth;
    }
}
