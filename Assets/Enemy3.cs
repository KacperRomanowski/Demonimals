using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : AbstractEnemy
{
    public float moveSpeed = 3f;
    public EnemyHealthBar healthBar;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 movement;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (player != null) {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle + 270f;
            direction.Normalize();
            movement = direction;
        }
        healthBar.changeHealth(health, 120f);
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player) {
            player.takeDamage(10);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        transform.localScale += new Vector3(0.1f, 0.1f, 0);
        moveSpeed += 1f;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
