using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
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
            enemy.TakeDamage(50f); // or any other damage value you want
            Destroy(gameObject);
        }
    }
}