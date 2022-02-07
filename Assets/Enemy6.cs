using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : AbstractEnemy
{
    public float moveSpeed = 3f;
    public EnemyHealthBar healthBar;
    public EnemyHealthBar staminaBar;
    public Rigidbody2D rb;
    public float stamina = 60f;
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
        healthBar.changeHealth(health, 20f);
        if (stamina < 60f) {
            stamina += 0.2f;
        }
        
        staminaBar.changeHealth(stamina, 60f);
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

    public void dodge()
    {
        var random = new System.Random();
        var randomNumber = random.Next(4);
        if (randomNumber == 0) {
            transform.position += new Vector3(1.0f, 1.0f, 0.0f);
        } else if (randomNumber == 2) {
            transform.position += new Vector3(-1.0f, -1.0f, 0.0f);
        } else if (randomNumber == 3) {
            transform.position += new Vector3(1.0f, -1.0f, 0.0f);
        } else {
            transform.position += new Vector3(-1.0f, 1.0f, 0.0f);
        }
        stamina -= 20;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
