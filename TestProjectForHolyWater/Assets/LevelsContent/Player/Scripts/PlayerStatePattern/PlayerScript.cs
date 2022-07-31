using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    public IPlayerBehaviorStand PlayerStandEvents { get; private set; }
    public IPlayerBehaviorMove PlayerMoveEvents { get; private set; }
    public IPlayerBehaviorFindPath PlayerFindPathEvents { get; private set; }
    public Vector3[] PathToMove { get; private set; }

    [SerializeField] private Animator _playerModelAnimator;

    private Dictionary<Type, IPlayerBehavior> _behaviorsMap;
    private IPlayerBehavior _behaviorCurrent;   
    
    [Inject] private ICollectablePool _collectablePool;

    private void Awake()
    {    
        InitializeBehaviors();
        SetBehaviorByDefault();        
    }

    private void OnDestroy()
    {
        _behaviorCurrent = null;
    }

    private void InitializeBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        _behaviorsMap[typeof(PlayerBehaviorStand)]      
            = new PlayerBehaviorStand(_playerModelAnimator, out IPlayerBehaviorStand playerBehaviorStand);
        PlayerStandEvents = playerBehaviorStand;

        _behaviorsMap[typeof(PlayerBehaviorMove)]      
            = new PlayerBehaviorMove(this, GetComponent<Rigidbody>(), _playerModelAnimator, transform, out IPlayerBehaviorMove playerBehaviorMove);
        PlayerMoveEvents = playerBehaviorMove;

        _behaviorsMap[typeof(PlayerBehaviorFindPath)]   
            = new PlayerBehaviorFindPath(transform, _collectablePool, out IPlayerBehaviorFindPath playerBehaviorFindPath);
        PlayerFindPathEvents = playerBehaviorFindPath;
    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Exit();

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }
   
    private void FixedUpdate()
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Update();
    }

    private void SetBehaviorByDefault() => SetBehaviorStand();

    public void SetBehaviorStand()
    {
        var behavior = GetBehavior<PlayerBehaviorStand>();
        SetBehavior(behavior);
    }

    public void SetBehaviorMove(Vector3[] path)
    {
        var behavior = GetBehavior<PlayerBehaviorMove>();
        PathToMove = path;
        SetBehavior(behavior);
    }

    public void SetBehaviorFindPath()
    {
        var behavior = GetBehavior<PlayerBehaviorFindPath>();
        SetBehavior(behavior);
    }
}
