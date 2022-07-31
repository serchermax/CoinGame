using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller 
{
    [SerializeField] private Transform _systemTransform;
    [SerializeField] private PlayerCollectablePool _playerCollectablePool;
    [SerializeField] private ScoreManager _scoreManager;
    
    public override void InstallBindings()
    {
        BindScoreManager();
        BindICollectable();        
    }

    private void BindICollectable()
    {
        PlayerCollectablePool playerCollectable 
            = Container.InstantiatePrefabForComponent<PlayerCollectablePool>
            (_playerCollectablePool, _systemTransform);

        Container.Bind<ICollectablePool>()
            .To<PlayerCollectablePool>()
            .FromInstance(playerCollectable)
            .AsSingle();
    }

    private void BindScoreManager()
    {
        ScoreManager scoreManager
            = Container.InstantiatePrefabForComponent<ScoreManager>
            (_scoreManager, _systemTransform);

        Container.Bind<ScoreManager>()
            .FromInstance(scoreManager)
            .AsSingle();
    }
}

