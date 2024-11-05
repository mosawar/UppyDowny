using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source ------")]
    // Reference for the audio source that will play background music
    [SerializeField] AudioSource musicSource;
    // Reference for the audio source that will play sound effects (SFX)
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Clips ------")]
    // Audio clip for background music
    public AudioClip background;
    // Audio clip for when a crow dies
    public AudioClip crowDeath;
    // Audio clip for crow sounds
    public AudioClip crow;
    // Audio clip for player's footsteps
    public AudioClip playerFootstep;
    // Audio clip for sword sounds
    public AudioClip sword;
    // Audio clip for crow attack sounds
    public AudioClip crowAttack;

    // Method to play a sound effect (SFX) using the SFX audio source
    // Takes an AudioClip as a parameter and plays it once
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
