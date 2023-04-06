using UnityEngine;

public class GravityWall : Player {
    private float floatingForce = 10f;
    void Start() {
        //gameObject.tag = "GravityWall";
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("GravityWall")) {
            rb.gravityScale = 1f;
            rb.AddForce(Vector2.up * floatingForce, ForceMode2D.Force);
            Debug.Log("entered gravity wall");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("GravityWall")) {
            rb.gravityScale = originalGravityScale;
            Debug.Log("exited gravity wall");
        }
    }
}
