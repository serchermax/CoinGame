using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorStand : IPlayerBehavior, IPlayerBehaviorStand
{
    private float _waitingTime = 1f;
    private float _timer = 0;

    private Animator _playerAnimator;

    public event Action OnWaitingEndEvent;

    public PlayerBehaviorStand(Animator playerAnimator, out IPlayerBehaviorStand playerBehaviorStand)
    {
        _playerAnimator = playerAnimator;
        playerBehaviorStand = this;
    }

    public void Enter()
    {
        _playerAnimator.SetBool("Grounded", true);
        _playerAnimator.SetFloat("MoveSpeed", 0);
    }

    public void Exit()
    {
        _timer = 0f;
    }

    public void Update()
    {
        if (_timer < _waitingTime) _timer += Time.deltaTime;
        else
        {
            OnWaitingEndEvent?.Invoke();
        }
    }
}

public interface IPlayerBehaviorStand
{
    public event Action OnWaitingEndEvent;
}