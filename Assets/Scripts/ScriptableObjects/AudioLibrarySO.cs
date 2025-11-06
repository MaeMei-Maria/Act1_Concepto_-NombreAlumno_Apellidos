using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "AudioLibrarySO", menuName = "Scriptable Objects/AudioLibrary")]
public class AudioLibrarySo : ScriptableObject
{
    [Header("Background Music")]
    public AudioClip mainMenuTheme;
    public AudioClip level1Theme;

    [Header("Sound Effects")]
    public AudioClip playerGunShootSfx;
    public AudioClip playerClusterShootSfx;
    public AudioClip enemyShootSfx;
    public AudioClip grenadeSfx;
    public AudioClip ammoBoxSfx;
    public AudioClip medicalKitSfx;
    public AudioClip buttonSfx;

    public AudioClip hitSfx;
    public AudioClip deathSfx;
}