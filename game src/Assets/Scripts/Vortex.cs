using UnityEngine;

public class Vortex : MonoBehaviour
{
    private GameObject connectedVortex;

    public GameObject ConnectedVortex
    {
        get { return connectedVortex; }
        set { connectedVortex = value; }
    }

    public GameObject vortexPrefab;
    public KeyCode spawnKey = KeyCode.Q;
    private GameObject vortex1;
    private GameObject vortex2;

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnVortexes();
        }
    }

    void SpawnVortexes()
    {
        // Destroy any existing vortexes
        if (vortex1 != null)
        {
            Destroy(vortex1);
        }
        if (vortex2 != null)
        {
            Destroy(vortex2);
        }

        // Spawn two new vortexes at random positions
        Vector3 position1 = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        Vector3 position2 = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        vortex1 = Instantiate(vortexPrefab, position1, Quaternion.identity);
        vortex2 = Instantiate(vortexPrefab, position2, Quaternion.identity);

        // Connect the two vortexes
        Vortex vortex1Component = vortex1.GetComponent<Vortex>();
        Vortex vortex2Component = vortex2.GetComponent<Vortex>();
        vortex1Component.ConnectedVortex = vortex2;
        vortex2Component.ConnectedVortex = vortex1;
    }
}