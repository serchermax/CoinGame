using UnityEngine;
using Zenject;

public class PlayAudio : MonoBehaviour
{
    private enum AudioType
    {
        SFX = 0,
        Music = 1
    }

    protected bool audioPlayerMute => _audioPlayer.mute;

    [SerializeField] private AudioType _audioType;
    [SerializeField] private string _audioName;

    private IAudioPlayer _audioPlayer;

    [Inject]
    private void Construct(SFXPlayer sfxPlayer, MusicPlayer musicPlayer)
    {
        switch (_audioType)
        {
            case AudioType.SFX:     _audioPlayer = sfxPlayer;   break;
            case AudioType.Music:   _audioPlayer = musicPlayer; break;
        }
    }

    public void Play()
    {      
        _audioPlayer.PlayAudio(_audioName);
    }

    public void Play(string audioName)
    {
        _audioPlayer.PlayAudio(audioName);
    }
}

   
