using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SFXPlayer _sfxPlayer;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private SceneTransitor _sceneTransitor;

    public override void InstallBindings()
    {
        BindSfxPlayer();
        BindMusicPlayer();
        BindSceneTransitor();
    }

    private void BindSfxPlayer()
    {
        SFXPlayer sfxPlayerInstance = Container.InstantiatePrefabForComponent<SFXPlayer>(_sfxPlayer);

        Container.Bind<SFXPlayer>()
            .FromInstance(sfxPlayerInstance)
            .AsSingle();
        DontDestroyOnLoad(sfxPlayerInstance.gameObject);
    }

    private void BindMusicPlayer()
    {
        MusicPlayer musicPlayerInstance = Container.InstantiatePrefabForComponent<MusicPlayer>(_musicPlayer);

        Container.Bind<MusicPlayer>()
            .FromInstance(musicPlayerInstance)
            .AsSingle();
        DontDestroyOnLoad(musicPlayerInstance.gameObject);
    }

    private void BindSceneTransitor()
    {
        SceneTransitor sceneTransitor = Container.InstantiatePrefabForComponent<SceneTransitor>(_sceneTransitor);

        Container.Bind<SceneTransitor>()
            .FromInstance(sceneTransitor)
            .AsSingle();
        DontDestroyOnLoad(sceneTransitor.gameObject);
    }
}
