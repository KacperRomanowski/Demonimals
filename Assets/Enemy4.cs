using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float health = 300f;
    public EnemyHealthBar healthBar;
    private Rigidbody2D rb;
    private Transform allyTransform;
    private Vector2 movement;
    private GameObject[] enemies;
    private AbstractEnemy allyToFollow;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        findAllyToHeal();

        if (allyTransform != null) {
            Vector3 direction = allyTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 270f;
            direction.Normalize();
            movement = direction;
            moveSpeed = 10f;
            if (Vector3.Distance(allyTransform.position, transform.position) <= 1f) {
                moveSpeed = 1f;
                if (! allyToFollow.isFullHealth()) {
                    allyToFollow.heal();
                }
            }
        } else {
            Destroy(gameObject);
        }
    }

    private void findAllyToHeal()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) {
            Destroy(gameObject);
        }

        if (! allyToFollow) {
            allyToFollow = enemies[0].GetComponent<AbstractEnemy>();
            allyTransform = enemies[0].transform;
        }

        foreach (GameObject enemy in enemies) {
            var myEnemy = enemy.GetComponent<AbstractEnemy>();

            if (myEnemy && myEnemy.health < allyToFollow.health && ! myEnemy.isFullHealth()) {
                allyToFollow = myEnemy;
                allyTransform = enemy.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.changeHealth(health, 100f);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var bullet = collider.GetComponent<Bullet>();
        if (bullet) {
            takeDamage(20);
        }
    }
}
