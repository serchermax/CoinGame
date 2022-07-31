using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab { get; private set; }
    public Transform Container { get; private set; }

    private List<T> _pool;
    private DiContainer _diContainer;

    public PoolMono(T prefab, int count, Transform container, DiContainer diContainer)
    {
        Prefab = prefab;
        Container = container;
        _diContainer = diContainer;

        CreatePool(count);
    }
    
    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveAfterInstance = false)
    {
        var createdObject = _diContainer.InstantiatePrefabForComponent<T>(Prefab, Container);
        createdObject.gameObject.SetActive(isActiveAfterInstance);
        _pool.Add(createdObject);
        return createdObject;
    }

    public bool TryGetObjectFromPool(out T poolObject)
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                poolObject = obj;
                poolObject.gameObject.SetActive(true);
                return true;
            }
        }
        poolObject = null;
        return false;
    }

    public T GetObjectFromPool()
    {
        if (TryGetObjectFromPool(out var poolObject))
        {
            return poolObject;
        }

        else return CreateObject(true);
    }
}
