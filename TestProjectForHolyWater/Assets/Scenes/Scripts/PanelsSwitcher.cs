using System;
using System.Collections;
using UnityEngine;

public class PanelsSwitcher : MonoBehaviour, IPanelSwitcher
{
    public event Action<bool> OnPanelSwitchEvent;

    private Animator _animator;

    [SerializeField] private AnimationClip _mainToSettings;
    [SerializeField] private AnimationClip _settingsToMain;

    private void Awake() { _animator = GetComponent<Animator>(); }

    public void OnSettingsButtonPressed()
    {
        StartCoroutine(PlayAnimation(_mainToSettings));
    }
    public void OnBackFromSettingsButtonPressed()
    {    
        StartCoroutine(PlayAnimation(_settingsToMain));
    }

    private IEnumerator PlayAnimation(AnimationClip animation)
    {
        OnPanelSwitchEvent?.Invoke(false);
        _animator.Play(animation.name);

        yield return new WaitForSeconds(animation.length);
        OnPanelSwitchEvent?.Invoke(true);        
    }
}
