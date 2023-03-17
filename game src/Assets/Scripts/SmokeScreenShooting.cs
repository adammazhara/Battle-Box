using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreenShooting : MonoBehaviour {
    public GameObject smokeScreenPrefab;
    public Transform smokeScreenShootingPoint;

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            Instantiate(smokeScreenPrefab, smokeScreenShootingPoint.position, smokeScreenShootingPoint.rotation);
        }
    }
}
