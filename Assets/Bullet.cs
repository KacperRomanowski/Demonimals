using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<Enemy>();
        var enemy2 = collision.collider.GetComponent<Enemy2>();
        var enemy3 = collision.collider.GetComponent<Enemy3>();
        var enemy5 = collision.collider.GetComponent<Enemy5>();
        var enemy6 = collision.collider.GetComponent<Enemy6>();
        if (enemy) {
            enemy.takeDamage(20);
        }
        if (enemy2) {
            enemy2.takeDamage(20);
        }
        if (enemy3) {
            enemy3.takeDamage(20);
        }
        if (enemy5) {
            enemy5.takeDamage(20);
        }
        if (enemy6) {
            if (enemy6.stamina > 0) {
                enemy6.dodge();
                return;
            }
            enemy6.takeDamage(20);
        }

        Destroy(gameObject);
    }
}