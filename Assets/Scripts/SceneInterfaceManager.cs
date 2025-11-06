using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneInterfaceManager : MonoBehaviour
{
    [SerializeField] private SceneManagerSo _sceneManager;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    
    public void LoadInGameScene()
    {
        PlaySFXButton();
        SceneManager.LoadScene(_sceneManager.InGameSceneName);
    }

    public void QuitGame()
    {
        PlaySFXButton();
        Application.Quit();
    }

    private void PlaySFXButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.audioLibrary.buttonSfx);
    }
}
