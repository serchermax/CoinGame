using UnityEngine;

public class PlayerPathDebugger : MonoBehaviour
{
    [SerializeField] private bool _onDebug;
    private Vector3[] _path;
    private Vector3 _startAngle;
    private Vector3 _endAngle;

    private void OnEnable()
    {
        PathConstructor.OnBezierCompleteForDebug += ModCreator;   
    }

    private void OnDisable()
    {
        PathConstructor.OnBezierCompleteForDebug += ModCreator;
    }

    private void ModCreator(Vector3[] path, Vector3 startAngle, Vector3 endAngle)
    {      
        _path = path;
        _startAngle = startAngle; 
        _endAngle = endAngle;
    }

    private void OnDrawGizmos()
    {
        if (!_onDebug || _path == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < _path.Length; i++)
        {
            Gizmos.DrawSphere(_path[i], 0.2f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_path[0], _startAngle);
        Gizmos.DrawLine(_endAngle, _path[_path.Length-1]);
    }
}
