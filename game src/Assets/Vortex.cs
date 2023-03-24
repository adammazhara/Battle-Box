using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vortex : MonoBehaviour
{
    private GameObject connectedVortex;


    public void setConnectedVortex(GameObject vortex)
    {
        connectedVortex = vortex;
    }


    public GameObject getConnectedVortex()
    {
        return connectedVortex;
    }
}

