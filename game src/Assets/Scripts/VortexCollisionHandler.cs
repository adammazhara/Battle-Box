using UnityEngine;
using System.Collections;

public class VortexCollisionHandler : MonoBehaviour
{
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            Vortex vortex = GetComponent<Vortex>();
            if (vortex != null)
            {
                GameObject otherVortex = vortex.ConnectedVortex;
                if (otherVortex != null)
                {
                    StartCoroutine(TeleportWithDelay(other, otherVortex));
                }
            }
        }
    }

    private IEnumerator TeleportWithDelay(Collider2D playerCollider, GameObject otherVortex)
    {
        canTeleport = false;
        playerCollider.transform.position = otherVortex.transform.position;
        VortexCollisionHandler otherHandler = otherVortex.GetComponent<VortexCollisionHandler>();
        if (otherHandler != null)
        {
            otherHandler.canTeleport = false;
        }
        yield return new WaitForSeconds(1f);
        canTeleport = true;
        if (otherHandler != null)
        {
            otherHandler.canTeleport = true;
        }
    }
}