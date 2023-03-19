using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMatch : MonoBehaviour
{
    public bool isHosting = false;

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
