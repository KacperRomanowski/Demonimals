using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidKitSpawner : MonoBehaviour
{
    public Transform firstAidKit;

    public void SpawnFirstAidKit()
    {
        Instantiate(firstAidKit);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Wave completed");
        var player = collision.collider.GetComponent<Player>();
        if (player) {
            player.health = 100f;;
            Destroy(gameObject);
        }
    }
}
