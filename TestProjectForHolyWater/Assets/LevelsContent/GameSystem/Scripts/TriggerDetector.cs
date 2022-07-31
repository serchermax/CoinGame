using System;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] private string[] _tagsForDetection;
    [SerializeField] private bool _detectAlways;

    public event Action<Transform, string> OnTriggerEnterInfoEvent;
    public event Action OnTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (_detectAlways)
        {
            OnTriggerEnterInfoEvent?.Invoke(other.transform, tag);
            OnTriggerEnterEvent?.Invoke();
        }
        else
        {
            foreach (string tag in _tagsForDetection)
            {
                if (tag == other.tag)
                {
                    OnTriggerEnterInfoEvent?.Invoke(other.transform, tag);
                    OnTriggerEnterEvent?.Invoke();
                    break;
                }
            }
        }
    }
}
