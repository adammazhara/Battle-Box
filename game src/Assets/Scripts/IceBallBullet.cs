using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallBullet : MonoBehaviour {
    [SerializeField] public float speed;
    private Rigidbody2D body;
    private bool hit;
    private float direction;
    private float lifetime;

    // Start is called before the first frame update
    private void Start() {
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * speed;
    }
    private void Update() {
        if (hit) return;

        lifetime += Time.deltaTime; // increment liftime by amount of time passed between current and last frames
        if (lifetime > 5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
    if (enemy != null) {
        enemy.TakeDamage(20f); // or any other damage value you want
        enemy.StartCoroutine(SlowDown(enemy, 0.5f, 3f)); // slowdown for 3 seconds
        enemy.StartCoroutine(ChangeColor(enemy, Color.blue, 1f)); // change color to blue for 0.5 seconds
        Destroy(gameObject);
    }
}

private IEnumerator SlowDown(Enemy enemy, float slowdownFactor, float duration) {
    float originalSpeed = enemy.speed;
    enemy.speed *= slowdownFactor;
    yield return new WaitForSeconds(duration);
    enemy.speed = originalSpeed;
}

private IEnumerator ChangeColor(Enemy enemy, Color color, float duration) {
    SpriteRenderer renderer = enemy.GetComponent<SpriteRenderer>();
    Color originalColor = renderer.color;
    renderer.color = color;
    yield return new WaitForSeconds(duration);
    renderer.color = originalColor;
}
}