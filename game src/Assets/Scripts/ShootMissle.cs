using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ShootMissle : MonoBehaviour {
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation); // create bullet object
        }
    }
}
