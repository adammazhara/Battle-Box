using UnityEngine;

public class AcidBall : MonoBehaviour
{
    [SerializeField] private GameObject puddlePrefab;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private float lifetime;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Create puddle and position it at the point of contact
            Vector3 contactPoint = collision.contacts[0].point;
            Instantiate(puddlePrefab, contactPoint, Quaternion.identity);

            // Destroy the acid ball
            Destroy(gameObject);
        }
    }
}
