using UnityEngine;

[RequireComponent(typeof(TriggerDetector))]
[RequireComponent(typeof(Rigidbody))]
public class ObjectGravityDisabler : MonoBehaviour
{
    [SerializeField] private float _yOffsetAfterGrounded = 1.25f;
    [SerializeField] private bool _onGravityAfterDisable = true;

    private TriggerDetector _triggerDetector;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _triggerDetector = GetComponent<TriggerDetector>();
        _triggerDetector.OnTriggerEnterInfoEvent += OffGravity;
    }

    private void OnDestroy()
    {
        _triggerDetector.OnTriggerEnterInfoEvent -= OffGravity;        
    }

    private void OnDisable()
    {
        if (_onGravityAfterDisable) OnGravity();
    }

    private void OffGravity(Transform triggeredTransform, string triggeredTag)
    {
        Vector3 newPos = transform.position;
        newPos.y = triggeredTransform.position.y + _yOffsetAfterGrounded;
        transform.position = newPos;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
    }

    private void OnGravity()
    {
        _rigidbody.useGravity = true;
    }
}
