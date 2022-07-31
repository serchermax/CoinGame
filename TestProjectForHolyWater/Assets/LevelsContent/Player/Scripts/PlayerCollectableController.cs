using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerDetector))]
public class PlayerCollectableController : MonoBehaviour
{
    private TriggerDetector _triggerDetector;
    private const string COLLECTABLE_TAG = "Collectable";

    private ScoreManager _scoreManager;

    [Inject]
    private void Construct(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    private void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
    }

    private void OnEnable()
    {
        _triggerDetector.OnTriggerEnterInfoEvent += OnTriggerCollectable;
    }

    private void OnDisable()
    {
        _triggerDetector.OnTriggerEnterInfoEvent -= OnTriggerCollectable;
    }

    private void OnTriggerCollectable(Transform collectableTransform, string collectableTag)
    {
        if (collectableTag == COLLECTABLE_TAG)
        {
            _scoreManager.AddScore(1);
            collectableTransform.gameObject.SetActive(false);
        }
    }
}
