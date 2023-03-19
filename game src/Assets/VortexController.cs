using UnityEngine;
using System.Collections;

public class VortexController : MonoBehaviour
{
    public GameObject vortexPrefab;
    public GameObject player;
    public GameObject vortex1;
    public GameObject vortex2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnVortexPair();
        }
    }

    void SpawnVortexPair()
    {
        if (vortex1 != null) Destroy(vortex1);
        if (vortex2 != null) Destroy(vortex2);

        vortex1 = Instantiate(vortexPrefab, player.transform.position, Quaternion.identity);
        Vector3 randomPosition = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);
        vortex2 = Instantiate(vortexPrefab, randomPosition, Quaternion.identity);
    }
}
