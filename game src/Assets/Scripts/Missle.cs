using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Transform target;
    private Rigidbody2D body;
    private float lifetime;

    // Start is called before the first frame update
    private void Start() {
        body = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Enemy>().transform; // assume an Enemy is the target
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (target == null) {
            return;
        }

        Vector2 direction = (Vector2)target.position - body.position;
        float angle = Vector2.SignedAngle(body.transform.right, direction);
        float rotation = Mathf.Sign(angle) * Mathf.Min(turnSpeed * Time.fixedDeltaTime, Mathf.Abs(angle));
        body.transform.Rotate(0, 0, rotation);
        body.velocity = body.transform.right * speed;

        lifetime += Time.deltaTime;
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

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }
}
