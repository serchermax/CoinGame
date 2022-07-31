using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractableSwitcher : MonoBehaviour
{
    private Selectable _selectable;
    private IPanelSwitcher _iPanelSwitcher;

    [Inject]
    private void Construct(IPanelSwitcher iPanelSwitcher)
    {       
        _iPanelSwitcher = iPanelSwitcher;
        _iPanelSwitcher.OnPanelSwitchEvent += SetSelectableInteractableState;
    }   

    private void OnDestroy()
    {
        _iPanelSwitcher.OnPanelSwitchEvent -= SetSelectableInteractableState;
    }

    private void Awake() { _selectable = GetComponent<Selectable>(); }

    public void Start() => SetSelectableInteractableState(true);

    public void SetSelectableInteractableState(bool state)
    {
        _selectable.interactable = state;
    }
}
