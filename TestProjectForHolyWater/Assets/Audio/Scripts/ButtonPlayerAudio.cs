using UnityEngine;
using UnityEngine.UI;

public class ButtonPlayerAudio : PlayAudio
{
    private Button _button;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick() => Play();
}
