using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private Transform target;
    private Rigidbody2D body;
    private float lifetime;
    private float startSequence; // amount of time for the first part of the flight before the missile acquires its target
    private bool startSequenceBool;
    private const float ACCELERATION = 1.0f;
    private const float MAX_SPEED = 30.0f;

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

        Vector2 heading = body.position - (Vector2)target.position;
        heading.Normalize();

        float value = Vector3.Cross(heading, transform.right).z;
        body.angularVelocity = turnSpeed * value;
        body.velocity = transform.right * speed;

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
