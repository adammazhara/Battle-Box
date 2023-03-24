using UnityEngine;
using System.Collections;


public class VortexCollisionHandler : MonoBehaviour
{
    public GameObject otherVortex;
    public float teleportDelay = 1f;
    private bool canTeleport = true;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            StartCoroutine(TeleportWithDelay(other));
        }
    }


    IEnumerator TeleportWithDelay(Collider2D playerCollider)
    {
        canTeleport = false;
        playerCollider.transform.position = otherVortex.transform.position;
        VortexCollisionHandler otherHandler = otherVortex.GetComponent<VortexCollisionHandler>();
        otherHandler.canTeleport = false;
        yield return new WaitForSeconds(teleportDelay);
        canTeleport = true;
        otherHandler.canTeleport = true;
    }
}
