using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(0f, Time.deltaTime * _rotationSpeed, 0f);        
    }
}
