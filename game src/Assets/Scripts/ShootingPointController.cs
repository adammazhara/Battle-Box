using UnityEngine;

public class ShootingPointController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject acidBallPrefab;
    [SerializeField] private GameObject iceBallPrefab; // new ice ball prefab variable
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float maxAngle = 90f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(bulletPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Shoot(acidBallPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.E)) // new ice ball key
        {
            Shoot(iceBallPrefab);
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

        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, shootingPointPos, Quaternion.identity);

        // Set projectile velocity
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = shootingDir * bulletSpeed;
    }
}