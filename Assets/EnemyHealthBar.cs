using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    public void changeHealth(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
