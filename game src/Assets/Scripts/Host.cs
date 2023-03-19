using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Host : MonoBehaviour
{
    public static Host Instance;

    public bool isHosting = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextScene()
    {
        Host.Instance.SetIsHosting(true);
        SceneManager.LoadScene(1);
    }

    public void SetIsHosting(bool value)
{
    isHosting = value;
}


}