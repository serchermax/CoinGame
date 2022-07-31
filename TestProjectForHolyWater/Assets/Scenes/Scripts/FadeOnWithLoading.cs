using UnityEngine;
using Zenject;

public class FadeOnWithLoading : FadeOn
{  
    [SerializeField] private bool _offAllInteractableSwitchers = true;

    private InteractableSwitcher[] _interactableSwitchers;
    private SceneTransitor _sceneTransitor;

    [Inject]
    private void Construct(SceneTransitor sceneTransitor)
    {
        _sceneTransitor = sceneTransitor;
        _sceneTransitor.OnSceneTransite += StartFading;
    }

    private void OnDestroy()
    {
        _sceneTransitor.OnSceneTransite -= StartFading;
    }

    protected override void Awake()
    {
        base.Awake();
        _interactableSwitchers = GetComponentsInChildren<InteractableSwitcher>();
    }

    protected override void StartFading()
    {
        if (_offAllInteractableSwitchers)
        {
            foreach (InteractableSwitcher interactableSwitcher in _interactableSwitchers)
            {
                interactableSwitcher.SetSelectableInteractableState(false);
            }
        }
        base.StartFading();
    }

    protected override void AfterFading() { } 
}
