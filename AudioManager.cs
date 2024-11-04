using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------- Audio Source ------")]
    public AudioClip background;
    public AudioClip crowDeath;
    public AudioClip crow;
    public AudioClip playerFootstep;
    public AudioClip sword;
    public AudioClip crowAttack;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
