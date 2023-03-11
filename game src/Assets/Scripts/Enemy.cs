using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 100f;
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float damageTimer;

    // Start is called before the first frame update
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.yellow;
    }

    // Update is called once per frame
    private void Update() {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        damageTimer += Time.deltaTime;

        if (damageTimer > 0.3)
            sr.color = Color.yellow;
    }

    public void TakeDamage(float damage) {
        if (health <= 0)
            Destroy(gameObject);
        health -= damage;
        sr.color = Color.red;
        damageTimer = 0;
    }

    // doesnt do anything, commented out in case needed later
    /*private void OnCollisionEnter2D(Collision2D collision) {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null) {
            TakeDamage(50f); // or any other damage value you want
            Destroy(collision.gameObject);
        }
    }*/
}