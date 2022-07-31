using UnityEngine;

public interface ICollectablePool
{
    public void AddTransformToCollectablePool(Transform transform, string tag);
    public bool TryGetRandomTransform(out Transform transform);
    public void DeleteTransformFromCollectablePool(Transform transform, string tag);
}
