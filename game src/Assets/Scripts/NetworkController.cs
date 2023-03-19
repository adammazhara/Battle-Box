using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Host.Instance.isHosting);
        if (Host.Instance.isHosting)
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Server is now hosting with Unity Netcode");
        }
        else
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Client is now attempting to join a random host");
        }
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Client connected: " + clientId);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log("Client disconnected: " + clientId);
    }
}
