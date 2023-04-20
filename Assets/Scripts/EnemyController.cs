using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;  // Movement speed of the enemy
    public int health = 1;        // Health of the enemy
    public bool isSpecial = false;// Will it spawn an upgrade?
    public GameObject upgradeItem;

    private void Update()
    {
        if (isSpecial)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        // Move the enemy to the left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Destroy the enemy if it goes off screen to the left
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (isSpecial)
        {
            SpawnUpgradeItem();
        }
    }
    private void SpawnUpgradeItem()
    {
        GameObject item = Instantiate(upgradeItem, transform.position, Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the enemy collides with a bullet, decrease its health by 1
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Reduce the enemy's health by a certain amount (e.g. 1)
            health--;

            // Destroy the bullet
            Destroy(collision.gameObject);

            // If the enemy's health is zero or below, destroy it
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
