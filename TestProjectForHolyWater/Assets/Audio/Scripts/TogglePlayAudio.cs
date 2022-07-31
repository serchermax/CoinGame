using UnityEngine.UI;

public class TogglePlayAudio : PlayAudio
{
    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnValueChanged);        
    }

    private void Start()
    {
        _toggle.isOn = !audioPlayerMute;
    }

    private void OnValueChanged(bool value) => Play();
}
