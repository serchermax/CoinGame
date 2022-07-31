using UnityEngine;

public class SFXPlayer : MonoBehaviour, IAudioPlayer
{
    [Range(0, 1f)]
    [SerializeField] private float maxVolume = 1f;
     
    public bool mute 
    {
        get { return _audioSource.volume == 0; }
    }

    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _clips = Resources.LoadAll<AudioClip>("SFX");
        MuteAudio(false);
    }

    public void MuteAudio(bool state)
    {
        if (state) _audioSource.volume = 0;   
        else _audioSource.volume = maxVolume;
    }

    public void PlayAudio(string name, object obj = null)
    {
        foreach (AudioClip audioClip in _clips)
        {
            if (audioClip.name == name)
            {
                _audioSource.clip = audioClip;
                _audioSource.Play();
                return;
            }
        }
        Debug.LogError("SFXAudioClip " + name + " not found! Object is " + obj);
    }
}
