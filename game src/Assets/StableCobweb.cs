using UnityEngine;

public class StableCobweb : MonoBehaviour
{
    [SerializeField] private float duration = 10f; // duration in seconds
    [SerializeField] private float slowFactor = 0.5f; // amount by which cobweb slows down opponents

    private void Start()
    {
         // destroy stable cobweb object after specified duration
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // check if collided with player
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.SlowDownMovement(slowFactor); // slow down player movement by slow factor
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // check if exited from player collider
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.RestoreMovement(); // restore player movement to normal
            }
        }
    }

    private void OnDestroy()
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.RestoreMovement(); // restore player movement to normal
        }
    }
}
