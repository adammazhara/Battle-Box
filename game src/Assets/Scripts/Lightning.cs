using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
    public GameObject lightningPrefab;
    public LayerMask enemyLayer;
    public float damage = 10f;
    public float areaRadius = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Spawn a lightning bolt at the mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            GameObject lightning = Instantiate(lightningPrefab, new Vector3(mousePos.x, 40, 0), Quaternion.identity);

            // Find all colliders within the area of the lightning bolt
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, areaRadius);

            // Apply damage to enemies within the area of the lightning bolt
            foreach (Collider2D collider in colliders)
            {
                if (enemyLayer == (enemyLayer | (1 << collider.gameObject.layer)))
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                    }
                }
            }

            // Make the lightning bolt move down slowly
            Rigidbody2D rb = lightning.GetComponent<Rigidbody2D>();
            float yVelocity = -30f;
            GameObject[] lightningBolts = new GameObject[9];
            for (int i = 1; i <= 9; i++)
            {
                yVelocity -= 0.5f;
                Vector3 newPosition = new Vector3(mousePos.x, 40 - (i * 5 + (3 * i)), 0);
                GameObject newLightning = Instantiate(lightningPrefab, newPosition, Quaternion.identity);
                Rigidbody2D newRb = newLightning.GetComponent<Rigidbody2D>();
                newRb.velocity = new Vector2(0, yVelocity);
                lightningBolts[i - 1] = newLightning;
                StartCoroutine(DelaySpawn(newLightning));
            }
            StartCoroutine(DestroyLightning(lightning, lightningBolts));
        }
    }

    IEnumerator DelaySpawn(GameObject newLightning)
    {
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator DestroyLightning(GameObject lightning, GameObject[] lightningBolts)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(lightning);
        for (int i = 0; i < lightningBolts.Length; i++)
        {
            Destroy(lightningBolts[i]);
        }
    }
}
