using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0, 1f)] private float _volume = 0.45f;

    [Space]

    [SerializeField] private bool _finishClipOnLoadScene;

    private AudioSource _source;
    
    private void Start()
    {
        TryGetComponent(out AudioSource source);
        _source = source == null ? gameObject.AddComponent<AudioSource>() : source;
    }

    public void PlayClip()
    {
        if (_finishClipOnLoadScene)
        {
            PlayClipDontDestroyOnLoad();
        }
        else
        {
            PlayClipDestroyOnLoad();
        }
    }
    
    
    public void PlayClip(AudioClip clip)
    {
        var temp = _clip;
        _clip = clip;
        
        PlayClip();
        
        _clip = temp;
    }

    private void PlayClipDestroyOnLoad()
    {
        if(_source.isPlaying) _source.Stop();
        _source.clip = _clip;
        _source.volume = _volume;
        _source.Play();
    }

    private void PlayClipDontDestroyOnLoad()
    {
        AudioSource audioSource = Instantiate(new GameObject()).AddComponent<AudioSource>();
        DontDestroyOnLoad(audioSource);

        audioSource.clip = _clip;
        audioSource.volume = _volume;
        audioSource.Play();
        StartCoroutine(audioSource.DestroyAudioOnFinish());
    }
}
