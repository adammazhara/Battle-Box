using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreen : MonoBehaviour {
    private Rigidbody2D body;
    private float lifetime;
    [SerializeField] public float speed;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * speed;

        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Collider2D projectileCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, projectileCollider);
    }

    private void Update() {
        lifetime += Time.deltaTime;
        if (lifetime > 40)
            Destroy(gameObject);
    }

    // bounce off walls
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall")) {
            ContactPoint2D[] contacts = new ContactPoint2D[1]; // make array size 1 for storing points of contact
            collision.GetContacts(contacts); // populate array with contact points
            Vector2 normalVector = (Vector2)contacts[0].normal; // get the normal vector of the contact point
            Vector2 reflectedVector = (Vector2)Vector3.Reflect(transform.forward, normalVector); // create a reflected vector
            body.velocity = reflectedVector * speed; // set projectile velocity to the reflected vector
        }
    }   
}
