using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;
    private float nextFireTime;
    public Animator animator;

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            animator.SetBool("Down", true);
        }
        else
        {
            animator.SetBool("Down", false);
        }
        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            animator.SetBool("Up", true);
        }
        else
        {
            animator.SetBool("Up", false);

        }

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with an enemy, destroy both the player and the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("Death", true);

            // Destroy the player and the enemy
            Destroy(collision.gameObject);
            Destroy(gameObject, 0.5f);            
        }
    }
    private void Shoot()
    {
        if (Input.GetButton("Jump"))
        {
            if (Time.time >= nextFireTime)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
                Destroy(bullet, 1f);
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            // Reset the fire rate timer when the space bar is released
            nextFireTime = 0f;
        }
    }
}