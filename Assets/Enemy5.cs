using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : AbstractEnemy
{
    public float moveSpeed = 3f;
    public EnemyHealthBar healthBar;
    public Transform child;
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
        healthBar.changeHealth(health, 100f);
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
        if (health <= 0) {
            if (transform.localScale.x > 0.15f) {
                var newChild = Instantiate(child, transform.position - new Vector3(0, 1, 0), transform.rotation);
                newChild.GetComponent<Enemy5>().health = 100;
                newChild.localScale -= new Vector3(0.05f, 0.05f, 0);
                var newChild2 = Instantiate(child, transform.position + new Vector3(0, 1, 0), transform.rotation);
                newChild2.GetComponent<Enemy5>().health = 100;
                newChild2.localScale -= new Vector3(0.05f, 0.05f, 0);   
            }
            Destroy(gameObject);
        }
    }
}
