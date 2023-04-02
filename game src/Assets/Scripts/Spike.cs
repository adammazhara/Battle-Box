using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    [SerializeField] public float damageAmount = 15f;
    [SerializeField] public float jumpAmount = 5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null) {
            player.TakeDamage(damageAmount); // or any other damage value you want
        }
}
}