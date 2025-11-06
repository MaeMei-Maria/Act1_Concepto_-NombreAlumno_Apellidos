using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public event Action OnSceneLoaded; 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(1);
    }

    public void NotifySceneLoaded()
    {
        OnSceneLoaded?.Invoke();
    }
}
