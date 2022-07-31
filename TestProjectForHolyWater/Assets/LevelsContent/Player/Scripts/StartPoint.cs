using UnityEngine;
using Zenject;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    private DiContainer _diContainer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    private void Start()
    {
        _diContainer.InstantiatePrefab(_playerPrefab, transform.position, Quaternion.identity, null);
    }
}
