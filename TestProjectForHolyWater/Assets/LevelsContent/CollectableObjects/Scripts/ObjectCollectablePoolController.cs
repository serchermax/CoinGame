using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerDetector))]
public class ObjectCollectablePoolController : MonoBehaviour
{
    private TriggerDetector _triggerDetector;
    private ICollectablePool _collectablePool;

    private const string COLLECTABLE_TAG = "Collectable";
    private const string UNTAGGED_TAG = "Untagged";

    [Inject]
    private void Construct(ICollectablePool collectablePool)
    {
        _collectablePool = collectablePool;
    }

    private void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
        _triggerDetector.OnTriggerEnterEvent += AddToCollectablePool;
    }

    private void OnDestroy()
    {
        _triggerDetector.OnTriggerEnterEvent -= AddToCollectablePool;
    }

    private void OnEnable()
    {
        tag = UNTAGGED_TAG;        
    }

    private void OnDisable()
    {
        if (tag == COLLECTABLE_TAG) _collectablePool.DeleteTransformFromCollectablePool(transform, tag);
    }

    private void AddToCollectablePool()
    {
        if (tag == UNTAGGED_TAG)
        {
            tag = COLLECTABLE_TAG;
            _collectablePool.AddTransformToCollectablePool(transform, tag);
        } 
    }
}
