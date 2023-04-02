using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D body;
    private bool hit;
    private float lifetime;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0f;

        Vector3 shootingPointPos = transform.position;
        shootingPointPos.z = 0f;

        Vector3 shootingDir = (mouseWorldPos - shootingPointPos).normalized;

        body = GetComponent<Rigidbody2D>();
        body.velocity = shootingDir * speed;
    }

    private void Update()
    {
        if (hit) return;

        lifetime += Time.deltaTime;
        if (lifetime > 5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(5f);
            StartCoroutine(SlowDown(enemy, 0f, 2f));
            StartCoroutine(ChangeColor(enemy, Color.blue, 1f));
            Destroy(gameObject);
        }
    }

    private IEnumerator SlowDown(Enemy enemy, float slowdownFactor, float duration)
    {
        float originalSpeed = enemy.speed;
        enemy.speed *= slowdownFactor;
        yield return new WaitForSeconds(duration);
        enemy.speed = originalSpeed;
    }

    private IEnumerator ChangeColor(Enemy enemy, Color color, float duration)
    {
        SpriteRenderer renderer = enemy.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        renderer.color = color;
        yield return new WaitForSeconds(duration);
        renderer.color = originalColor;
    }
}
