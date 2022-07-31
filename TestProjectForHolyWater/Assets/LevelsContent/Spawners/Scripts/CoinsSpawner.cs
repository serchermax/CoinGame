using System.Collections;
using UnityEngine;
using Zenject;

public class CoinsSpawner : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _startCountInPool = 1;
    [SerializeField] private Transform _poolContainer;
    
    [Header("Spawn Settings")]
    [SerializeField] private Vector3 _spawnZone;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnTimeModificator;
     
    [Header("Debug")] 
    [SerializeField] private bool _drawGizmos;

    private PoolMono<Coin> _coinPool;
    [Inject] private DiContainer _diContainer;

    private void Start()
    {
        _coinPool = new PoolMono<Coin>(_coinPrefab, _startCountInPool, _poolContainer, _diContainer);

        StartSpawn();
    }

    private void StartSpawn() => StartCoroutine(SpawnCoin());

    private IEnumerator SpawnCoin()
    {
        Coin newCoin = _coinPool.GetObjectFromPool();
        newCoin.transform.position = GetRandomPositionForSpawn();

        float cooldown = _spawnTime + Random.Range(-_spawnTimeModificator, _spawnTimeModificator);
        yield return new WaitForSeconds(cooldown);

        StartSpawn();
    }

    private Vector3 GetRandomPositionForSpawn()
    {
        Vector3 position = transform.position;
        position.x += Random.Range(-_spawnZone.x, _spawnZone.x);
        position.y += Random.Range(-_spawnZone.y, _spawnZone.y);
        position.z += Random.Range(-_spawnZone.z, _spawnZone.z);
        return position;
    }

    private void OnDrawGizmos()
    {
        if (!_drawGizmos) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _spawnZone*2);        
    }
}
