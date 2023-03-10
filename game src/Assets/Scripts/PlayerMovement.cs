using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D body;
    private bool grounded = false;

    [SerializeField] private float speed; // editable directly from unity

    // every time script is loaded
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start() {
        
    }

    // Update is called once per frame automatically
    // Horizontal "A": -1, "D": 1
    private void Update() {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetKey(KeyCode.Space) && grounded) { // if space bar pressed
            Jump();
        }
    }

    private void Jump() {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}
