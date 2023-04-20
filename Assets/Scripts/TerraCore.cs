using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TerraCore : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float shootDelay = 3f;
    public float laserSpeed = 3f;

    private Transform playerTransform;
    private bool canShoot = false;
    public GameObject projectilePrefab;
    public Transform laserSpawner;
    public Transform laserSpawner2;

    private void Start()
    {
        // Get the player transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!canShoot)
        {
            // Move towards the player vertically
            float step = moveSpeed * Time.deltaTime;
            Vector2 targetPosition = new Vector2(transform.position.x, playerTransform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            // Check if Terra Core has reached the player's y position
            if (transform.position.y == targetPosition.y)
            {
                canShoot = true;
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        Debug.Log("Terra Core shooting");
        GameObject laser = Instantiate(projectilePrefab, laserSpawner.position, Quaternion.identity);
        GameObject laser2 = Instantiate(projectilePrefab, laserSpawner2.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = transform.right * -laserSpeed;
        laser2.GetComponent<Rigidbody2D>().velocity = transform.right * -laserSpeed;
        yield return new WaitForSeconds(shootDelay);

        canShoot = false;
    }
}
