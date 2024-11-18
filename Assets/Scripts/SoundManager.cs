using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static void PlaySound(Sound sound, float volumen)
    {   
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volumen);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        
        return null;
    }
    public enum Sound
    {
        GolpeBolla,
        GolpeTronco,
        EntrarCamalote,
        Rebote,
        Click,
        BoteRoto,
        PowerUpVelocidad,
        Buque
    }
}
