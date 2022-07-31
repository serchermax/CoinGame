using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerScript))]
public class PlayerBehaviorSwitcher : MonoBehaviour
{
    private PlayerScript _playerScript;

    private void OnEnable()
    {
        _playerScript = GetComponent<PlayerScript>();

        _playerScript.PlayerStandEvents.OnWaitingEndEvent += OnWaitingEnd;
        _playerScript.PlayerMoveEvents.OnMoveEnd += OnMoveEnd;
        _playerScript.PlayerFindPathEvents.OnPathFoundEvent += OnPathFound;
        _playerScript.PlayerFindPathEvents.OnPathNotFoundEvent += OnPathNotFound;     
    }

    private void OnDisable()
    {
        _playerScript.PlayerStandEvents.OnWaitingEndEvent -= OnWaitingEnd;
        _playerScript.PlayerMoveEvents.OnMoveEnd -= OnMoveEnd;
        _playerScript.PlayerFindPathEvents.OnPathFoundEvent -= OnPathFound;
        _playerScript.PlayerFindPathEvents.OnPathNotFoundEvent -= OnPathNotFound;
    }

    private void OnPathFound(Vector3[] path)
    {
        _playerScript.SetBehaviorMove(path);
    }
    private void OnPathNotFound()
    {
        _playerScript.SetBehaviorStand();
    }

    private void OnMoveEnd()
    {
        _playerScript.SetBehaviorFindPath();
    }
    private void OnWaitingEnd()
    {
        _playerScript.SetBehaviorFindPath();
    }

}
