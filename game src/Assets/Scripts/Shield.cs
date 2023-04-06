using System.Collections;
using UnityEngine;

public class Shield : PlayerMovement2 {
    [SerializeField] private GameObject shieldPrefab;

    private float shieldCooldown = 10f;
    private int shieldCount = 0;
    private GameObject shield;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R) && shield == null) ActivateShield();
    }

    private void FixedUpdate() {
        if (shield != null){
            // calculate the offset based on facing direction
            float offset = isFacingRight ? 1f : -1f;

            // set the position of the shield just in front of the player
            shield.transform.position = transform.position + new Vector3(offset, 0f, 0f);
        }
    }

    private void ActivateShield() {
        if (!shieldActive && shieldCount < 3) {
            shieldCount++;
           
            if (!shieldActive) {
                // calculate the offset based on facing direction
                float offset = isFacingRight ? 1f : -1f;

                // spawn the shield just in front of the player
                Vector3 spawnPosition = transform.position + new Vector3(offset, 0f, 0f);

                // instantiate the shield
                shield = Instantiate(shieldPrefab, spawnPosition, Quaternion.Euler(0f, offset > 0f ? 0f : 180f, 0f));

                // set shield active flag to true and start coroutine to disable shield
                shieldActive = true;
                StartCoroutine(DisableShield());
            }
        }
    }

    private IEnumerator DisableShield() {
        yield return new WaitForSeconds(3f);
        shieldActive = false;
        Destroy(shield);
        yield return new WaitForSeconds(shieldCooldown);
    }

    public bool IsShieldActive() {
        return shieldActive;
    }
}