using System.Collections;
using UnityEngine;

public class CloneControl : Player {
    [SerializeField] private GameObject clonePrefab;
    private GameObject clone;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C) && clone == null) Clone();
    }

    private void FixedUpdate() {
        if (clone != null) clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
    }

    private void Clone() {
        if (clone == null) {
            Vector3 randomOffset = new Vector3(transform.position.x - 10f, transform.position.y + 1f, transform.position.z);
            clone = Instantiate(clonePrefab, randomOffset, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        } else {
            Vector3 randomOffset = new Vector3(transform.position.x - 10f, transform.position.y + 1f, transform.position.z);
            clone.transform.position = randomOffset;
            clone.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        }
    }
}