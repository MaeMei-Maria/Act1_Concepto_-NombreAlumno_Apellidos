using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int sfxPoolSize = 10;
    [SerializeField] private AudioSource musicSourcePrefab;
    [SerializeField] private AudioSource sfxSourcePrefab;
    [SerializeField] private AudioLibrarySo audioLibrary; // ← Nuevo ScriptableObject con tus clips

    private List<AudioSource> sfxPool = new();
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Crear fuente para música
        musicSource = Instantiate(musicSourcePrefab, transform);
        musicSource.loop = true;

        // Crear pool para SFX
        for (int i = 0; i < sfxPoolSize; i++)
        {
            AudioSource sfx = Instantiate(sfxSourcePrefab, transform);
            sfxPool.Add(sfx);
        }

        // Escuchar cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ------------------ MÚSICA ------------------ //

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambia música según el nombre de la escena
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(audioLibrary.mainMenuTheme, 0.8f);
                break;

            case "Level1":
                PlayMusic(audioLibrary.level1Theme, 0.7f);
                break;

            default:
                StopMusic();
                break;
        }
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void SetMusicVolume(float volume) =>
        musicSource.volume = Mathf.Clamp01(volume);

    // ------------------ EFECTOS ------------------ //

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        AudioSource available = sfxPool.Find(s => !s.isPlaying);
        if (available == null)
        {
            available = sfxPool[0];
            available.Stop();
        }

        available.clip = clip;
        available.volume = volume;
        available.Play();
    }
}
