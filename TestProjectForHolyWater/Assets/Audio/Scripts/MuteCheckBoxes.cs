using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MuteCheckBoxes : MonoBehaviour
{
    private enum AudioType
    {
        SFX = 0,
        Music = 1
    }

    [SerializeField] private AudioType _audioType;

    private IAudioPlayer _audioPlayer;
    private Toggle _toggle;

    [Inject]
    private void Construct(SFXPlayer sfxPlayer, MusicPlayer musicPlayer)
    {
        switch (_audioType)
        {
            case AudioType.SFX: _audioPlayer = sfxPlayer; break;
            case AudioType.Music: _audioPlayer = musicPlayer; break;
        }
    }

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        _audioPlayer.MuteAudio(!value);
    }
}
