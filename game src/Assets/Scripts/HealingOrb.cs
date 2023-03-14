using System.Collections;
using UnityEngine;

public class HealingOrb : MonoBehaviour
{
    [SerializeField] private float healingAmount = 25f;
    [SerializeField] private float orbDuration = 5f;
    [SerializeField] private float orbSpeed = 2f;
    [SerializeField] private float healingRadius = 2f;

    private bool isMoving = false;
    private Vector2 targetPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // check if player pressed the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            // spawn orb 5 units above the player
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 orbSpawnPosition = new Vector2(playerPosition.x, playerPosition.y + 5f);
            Instantiate(gameObject, orbSpawnPosition, Quaternion.identity);
        }

        if (isMoving)
        {
            // calculate direction towards target position
            Vector2 direction = (targetPosition - rb.position).normalized;

            // calculate distance to target position
            float distance = Vector2.Distance(rb.position, targetPosition);

            // check if the orb has reached the target position
            if (distance < 0.1f)
            {
                // stop moving and start orb duration timer
                isMoving = false;
                StartCoroutine(OrbDurationTimer());
            }
            else
            {
                // move the orb towards the target position
                rb.MovePosition(rb.position + direction * orbSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // check if player is within healing radius
            Vector2 playerPosition = collision.transform.position;
            float distance = Vector2.Distance(playerPosition, transform.position);
            if (distance <= healingRadius)
            {
                // heal the player
                PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.Heal(healingAmount * Time.deltaTime);
                }
            }
        }
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Heal(healingAmount);
            Destroy(gameObject);
        }
    }
}

    private IEnumerator OrbDurationTimer()
    {
        // wait for the orb duration to expire before destroying the orb
        yield return new WaitForSeconds(orbDuration);
        Destroy(gameObject);
    }

    public void StartMoving(Vector2 target)
    {
        // set the target position and start moving
        targetPosition = target;
        isMoving = true;
    }
}
