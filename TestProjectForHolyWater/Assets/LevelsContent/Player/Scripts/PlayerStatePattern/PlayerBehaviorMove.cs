using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBehaviorMove : IPlayerBehavior, IPlayerBehaviorMove
{
    private PlayerScript _playerScript;
    private Rigidbody _rigidbody;
    private Transform _playerTransform;
    private Animator _playerAnimator;

    private float _speed = 5f;

    private Vector3[] _path;
    private int _pathIndex;

    public event Action OnMoveEnd;

    public PlayerBehaviorMove(PlayerScript playerScript, Rigidbody rigidbody, Animator playerAnimator, Transform playerTransform, out IPlayerBehaviorMove playerBehaviorMove)
    {        
        _playerScript = playerScript;
        _rigidbody = rigidbody;
        _playerAnimator = playerAnimator;
        _playerTransform = playerTransform;
        playerBehaviorMove = this;
    }

    public void Enter()
    {
        _path = _playerScript.PathToMove;
        _pathIndex = 0;

        if (_path == null)
        {
            Debug.Log("Path is wrong!");
            OnMoveEnd?.Invoke();
        }
    }

    public void Exit()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void Update()
    {
        if (Vector3.Distance(_playerTransform.position, _path[_pathIndex]) < _speed * Time.deltaTime)
        {
            _pathIndex++;

            if (_pathIndex >= _path.Length)
            {
                OnMoveEnd?.Invoke();
                return;
            }
        }
        
        _playerAnimator.SetFloat("MoveSpeed", 1);

        _playerTransform.LookAt(_path[_pathIndex], Vector3.up);
        Vector3 temp = _playerTransform.eulerAngles;
        temp.x = 0;
        temp.z = 0;
        _playerTransform.eulerAngles = temp;        

        Vector3 movePosition = Vector3.MoveTowards(_playerTransform.position, _path[_pathIndex], _speed * Time.deltaTime);
        _rigidbody.MovePosition(movePosition);
    }
}

public interface IPlayerBehaviorMove
{
    public event Action OnMoveEnd;
}