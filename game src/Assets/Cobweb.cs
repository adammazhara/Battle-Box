using UnityEngine;

public class Cobweb : MonoBehaviour
{
    [SerializeField] private float duration = 10f; // duration in seconds
    [SerializeField] private float decayRate = 0.2f; // rate at which slow factor decays over time
    [SerializeField] private GameObject stableCobwebPrefab; // prefab for stable cobweb object

    private float currentSlowFactor; // current slow factor, which gradually decays over time
    private PlayerMovement affectedPlayer; // reference to the affected player

    private void Start()
    {
        currentSlowFactor = 0f;
        Destroy(gameObject, duration); // destroy cobweb object after specified duration
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // check if collided with player
        {
            affectedPlayer = collision.GetComponent<PlayerMovement>(); // store reference to player movement component
            if (affectedPlayer != null)
            {
                affectedPlayer.SlowDownMovement(currentSlowFactor * 5); // slow down player movement by current slow factor
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // check if collided with ground
        {
            if (affectedPlayer != null)
            {
                affectedPlayer.SetVelocityZero(); // stop player movement
            }

            // spawn stable cobweb object and destroy cobweb object
            if (stableCobwebPrefab != null)
            {
                GameObject stableCobweb = Instantiate(stableCobwebPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("StableCobweb prefab not found!");
            }
        }
    }

    private void Update()
    {
        currentSlowFactor -= decayRate * Time.deltaTime; // gradually decrease slow factor over time
        currentSlowFactor = Mathf.Max(currentSlowFactor, 0f); // ensure slow factor is non-negative
    }

    private void OnDestroy()
    {
        if (affectedPlayer != null)
        {
            affectedPlayer.RestoreMovement(); // restore player movement when cobweb is destroyed
        }
    }
}
