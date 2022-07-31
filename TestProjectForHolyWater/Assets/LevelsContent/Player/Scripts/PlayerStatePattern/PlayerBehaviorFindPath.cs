using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerBehaviorFindPath : IPlayerBehavior, IPlayerBehaviorFindPath
{
    private ICollectablePool _collectablePool;
    private Transform _playerTransform;

    private float _findingRepeatTime = 1f;
    private float _timer = 0;

    public event Action<Vector3[]> OnPathFoundEvent;
    public event Action OnPathNotFoundEvent;

    public PlayerBehaviorFindPath(Transform playerTransform, ICollectablePool collectablePool, out IPlayerBehaviorFindPath playerBehaviorFindPath)
    {
        _playerTransform = playerTransform;
        _collectablePool = collectablePool;
        playerBehaviorFindPath = this;
    }

    public void Enter()
    {
        MakePath();
    }

    public void Exit()
    {
        _timer = 0;
    }

    public void Update()
    {
        if (_timer < _findingRepeatTime) _timer += Time.deltaTime;
        else
        {
            MakePath();
        }
    }

    private void MakePath()
    {
        if (_collectablePool.TryGetRandomTransform(out Transform collectableTransform))
        {
            PathConstructor pathConstructor = new PathConstructor(_playerTransform.position, collectableTransform.position);
            OnPathFoundEvent?.Invoke(pathConstructor.Path);
        }
        else
        {
            OnPathNotFoundEvent?.Invoke();
        }
    }       
}

public interface IPlayerBehaviorFindPath
{
    public event Action<Vector3[]> OnPathFoundEvent;
    public event Action OnPathNotFoundEvent;
}