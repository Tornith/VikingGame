using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OarAudioController : MonoBehaviour
{
    public AudioClip[] oarSounds;
    public AudioSource[] oarAudioSources;
    
    public float oarVolume = 0.5f;
    public float oarVolumeVariation = 0.1f;
    public float oarPitch = 1.0f;
    public float oarPitchVariation = 0.1f;

    public void PlayOarSound(int oarIndex)
    {
        oarAudioSources[oarIndex].clip = oarSounds[Random.Range(0, oarSounds.Length)];
        oarAudioSources[oarIndex].volume = oarVolume + Random.Range(-oarVolumeVariation, oarVolumeVariation);
        oarAudioSources[oarIndex].pitch = oarPitch + Random.Range(-oarPitchVariation, oarPitchVariation);
        oarAudioSources[oarIndex].Play();
    }
    
    public void PlayAllOarSounds()
    {
        for (var i = 0; i < oarAudioSources.Length; i++)
        {
            PlayOarSound(i);
        }
    }
}
