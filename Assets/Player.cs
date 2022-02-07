using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 100f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePosition;

    void Start()
    {
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var firstAidKit = collision.collider.GetComponent<FirstAidKit>();
        if (firstAidKit) {
            health = 100f;;
            Destroy(firstAidKit);
        }
    }
}
