using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausarMusica()
    {
        musicSource.pitch = 0.65f;
    }

    public void StartMusica()
    {
        musicSource.pitch = 1;
        if (!musicSource.isPlaying)
                musicSource.Play();
    }
}
