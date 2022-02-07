using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : AbstractEnemy
{
    public float moveSpeed = 3f;
    public float timeBetweenShots;
    public float range;
    public EnemyHealthBar healthBar;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 15f;
    private Rigidbody2D rb;
    private Transform player;
    private Vector2 movement;
    private bool followPlayer = true;
    private float distToPlayer;
    private float cooldownTimer = 1.5f;

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

            distToPlayer = Vector2.Distance(transform.position, player.position);

            cooldownTimer -= Time.deltaTime;
            if (distToPlayer <= range) {
                if (cooldownTimer > 0) {
                    return;
                }
                cooldownTimer = 1.5f;
                followPlayer = false;
                rb.velocity = Vector2.zero;
                Shoot();
            } else {
                followPlayer = true;
            }
        } else {
            followPlayer = false;
        }

        healthBar.changeHealth(health, 80f);
    }

    private void FixedUpdate()
    {
        if (followPlayer) {
            MoveCharacter(movement);
        }
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
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
