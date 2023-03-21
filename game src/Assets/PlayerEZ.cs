using System.Collections;
using UnityEngine;

public class PlayerEZ : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    public GameObject player; // Reference the player GameObject in the Inspector
    private float speed = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;

    private float decisionDelay = 0.5f;
    private float timeToNextDecision;
    private float shootCooldown = 1.5f;
    private float timeToNextShot;
    private float dodgeSpeed = 20f;
    private LayerMask bulletLayer;

    void Start()
    {
        timeToNextDecision = decisionDelay;
        bulletLayer = LayerMask.GetMask("Bullet");
    }

    private void Update()
    {
        timeToNextDecision -= Time.deltaTime;
        timeToNextShot -= Time.deltaTime;

        if (timeToNextDecision <= 0)
        {
            MakeDecision();
            timeToNextDecision = decisionDelay;
        }

        if (timeToNextShot <= 0)
        {
            ShootAtPlayer();
            timeToNextShot = shootCooldown;
        }

        DodgeBullets();
    }

    private void MakeDecision()
    {
        // Flip if necessary
        Flip();

        // Adjust decisionDelay based on the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        decisionDelay = Mathf.Clamp(1f - (distanceToPlayer / 10f), 0.2f, 1f);
    }

    private void Flip()
    {
        if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        float bulletSpeed = 10f;
        bulletRb.velocity = direction * bulletSpeed;
        Destroy(bullet, 3f);
    }

    private void DodgeBullets()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, isFacingRight ? Vector2.right : Vector2.left, 5f, bulletLayer);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Bullet"))
            {
                float dodgeDirection = hitInfo.collider.transform.position.y > transform.position.y ? -1f : 1f;
                rb.velocity = new Vector2(rb.velocity.x, dodgeSpeed * dodgeDirection);
            }
        }
        else
        {
            // Reset vertical velocity when there is no bullet to dodge
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }
}
