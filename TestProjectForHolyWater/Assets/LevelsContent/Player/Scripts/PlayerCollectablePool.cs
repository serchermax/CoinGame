using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectablePool : MonoBehaviour, ICollectablePool
{
    public static ICollectablePool instance { get; private set; }

    private const string COLLECTABLE_TAG = "Collectable";
    private List<Transform> _collectablePool;   

    private void Awake()
    {
        instance = this;
        _collectablePool = new List<Transform>();
    }  

    public void AddTransformToCollectablePool(Transform transform, string tag)
    {
        if (tag == COLLECTABLE_TAG) _collectablePool.Add(transform);
        else Debug.LogWarning("Tag " + tag + " is wrong to Collect!");
    }

    public bool TryGetRandomTransform(out Transform transform)
    {
        if (_collectablePool.Count > 0)
        {
            transform = _collectablePool[Random.Range(0, _collectablePool.Count)];
            return true;
        }    
        else
        {
            transform = null;
            return false;
        }
    }

    public void DeleteTransformFromCollectablePool(Transform collectableTransform, string collectableTag)
    {        
        if (collectableTag == COLLECTABLE_TAG)
        {
            for (int i = 0; i < _collectablePool.Count; i++)
            {                
                if (_collectablePool[i] == collectableTransform)
                {
                    _collectablePool.RemoveAt(i);                    
                    return;
                }
            }
            Debug.Log("PlayerCollectable can't delete collectable " + collectableTransform + " from collectable pool!");
        }
        else Debug.LogWarning("Tag " + collectableTag + " is wrong to Collect!");
    }
}
