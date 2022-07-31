using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private PanelsSwitcher _panelsSwitcher;
    public override void InstallBindings()
    {
        BindIPanelSwitcher();
    }

    private void BindIPanelSwitcher()
    {
        Container.Bind<IPanelSwitcher>()
            .To<PanelsSwitcher>()
            .FromInstance(_panelsSwitcher)
            .AsSingle();
    }
}
