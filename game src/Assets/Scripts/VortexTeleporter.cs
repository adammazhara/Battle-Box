using UnityEngine;
using System.Collections;

public class VortexTeleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject vortexPrefab;
    private GameObject vortex1;
    private GameObject vortex2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnVortexPair();
        }
    }
    IEnumerator EnableColliderWithDelay(Collider2D collider, float delay)
{
    yield return new WaitForSeconds(delay);
    collider.enabled = true;
}

    void SpawnVortexPair()
{
    if (vortex1 != null) Destroy(vortex1);
    if (vortex2 != null) Destroy(vortex2);

    Vector3 playerPosition = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);
    Vector3 randomPosition = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);

    vortex1 = Instantiate(vortexPrefab, playerPosition, Quaternion.identity);
    vortex2 = Instantiate(vortexPrefab, randomPosition, Quaternion.identity);

    VortexCollisionHandler collisionHandler1 = vortex1.AddComponent<VortexCollisionHandler>();
    VortexCollisionHandler collisionHandler2 = vortex2.AddComponent<VortexCollisionHandler>();

    collisionHandler1.otherVortex = vortex2;
    collisionHandler2.otherVortex = vortex1;
}




    private class VortexCollisionHandler : MonoBehaviour
    {
        public GameObject otherVortex;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = otherVortex.transform.position;
            }
        }
    }
}
