using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private bool hit;
    private float lifetime;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    public void SetDirection(Vector2 direction)
    {
        body.velocity = direction.normalized * speed;
    }


    private void Update()
    {
        if (hit) return;


        lifetime += Time.deltaTime; // increment lifetime by amount of time passed between current and last frames
        if (lifetime > 5)
            Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(50f); // or any other damage value you want
            Destroy(gameObject);
        }
    }
}



//hi