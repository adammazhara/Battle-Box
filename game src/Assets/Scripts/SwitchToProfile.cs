using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class SwitchToProfile : MonoBehaviour
{
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.Singleton;
    }

    public void LoadScene(string scene)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Disconnect();
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            Disconnect();
        }

        SceneManager.LoadScene(scene);
    }

    void Disconnect()
{
    NetworkManager.Singleton.Shutdown();
}
}
