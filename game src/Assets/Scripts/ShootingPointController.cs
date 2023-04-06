using System;
using UnityEngine;

public class ShootingPointController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject acidBallPrefab;
    [SerializeField] private GameObject iceBallPrefab;
    [SerializeField] private GameObject cobwebPrefab; // new cobweb prefab variable
    [SerializeField] private GameObject movingWallPrefab; // new moving wall prefab variable
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float cobwebDuration = 5f; // new variable for cobweb duration

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Shoot(bulletPrefab);
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            Shoot(acidBallPrefab);
        } else if (Input.GetKeyDown(KeyCode.E)) {
            Shoot(iceBallPrefab);
        } else if (Input.GetKeyDown(KeyCode.J)) {
            Shoot(cobwebPrefab);
        } else if (Input.GetKeyDown(KeyCode.X)) {
            Shoot(movingWallPrefab);
        //} else if (Input.GetKeyDown(KeyCode.V)) {
            //Shoot();
        } else {
            return;
        }
    }

    private void Shoot(GameObject projectilePrefab)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0f;

        Vector3 shootingPointPos = transform.position;
        shootingPointPos.z = 0f;

        Vector3 shootingDir = (mouseWorldPos - shootingPointPos).normalized;
        float shootingAngle = Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg;
        shootingAngle = Mathf.Clamp(shootingAngle, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0f, 0f, shootingAngle);

        if (shootingAngle < 0f && playerTransform.localScale.x > 0f)
        {
            playerTransform.localScale = new Vector3(-playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        }
        else if (shootingAngle >= 0f && playerTransform.localScale.x < 0f)
        {
            playerTransform.localScale = new Vector3(-playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
        }

        GameObject projectileObject = Instantiate(projectilePrefab, shootingPointPos, Quaternion.identity);

        if (projectilePrefab == movingWallPrefab) // if the projectile is the moving wall, set its isFacingRight variable based on the player's direction
        {
            MovingWall movingWall = projectileObject.GetComponent<MovingWall>();
            if (movingWall != null)
            {
                movingWall.isFacingRight = playerTransform.localScale.x > 0f;
            }
        }

        Rigidbody2D projectileRb = projectileObject.GetComponent<Rigidbody2D>();
        projectileRb.velocity = shootingDir * bulletSpeed;
    }
}
