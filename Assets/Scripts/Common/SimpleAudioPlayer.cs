using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Simple Audio Player")]
public class SimpleAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private bool _playOnStart = true;
    [SerializeField] private float _minDelay = 1f;
    [SerializeField] private float _maxDelay = 3f;

    private AudioSource _audioSource;
    private float _nextPlayTime;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (_playOnStart)
        {
            PlayRandom();
        }
    }

    private void Update()
    {
        if (Time.time >= _nextPlayTime && _clips.Length > 0)
        {
            PlayRandom();
        }
    }

    private void PlayRandom()
    {
        AudioClip clip = _clips[Random.Range(0, _clips.Length)];
        _audioSource.PlayOneShot(clip);

        _nextPlayTime = Time.time + Random.Range(_minDelay, _maxDelay);
    }

    [ContextMenu("Test Play")]
    public void TestPlay()
    {
        PlayRandom();
    }
}