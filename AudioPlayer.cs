using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource _audioSource;
    private int currentClipId;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = clips[0];
        _audioSource.Play();
        currentClipId++;
        Invoke(nameof(PlayClip),_audioSource.clip.length+10);
    }

    private void PlayClip()
    {
        if (currentClipId > clips.Length) currentClipId = 0;
        _audioSource.clip = clips[currentClipId];
        _audioSource.Play();
        currentClipId++;
    }
}
