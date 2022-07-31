using UnityEngine;

public class PathConstructor 
{
    public static event System.Action<Vector3[], Vector3, Vector3> OnBezierCompleteForDebug;

    private float _distantion = 1f;
    private int _pointsCount = 20;

    public Vector3[] Path { get; private set; }

    public PathConstructor(Vector3 start, Vector3 end)
    {
        if (_pointsCount < 2) _pointsCount = 2;
        BezierPath(start, end);
    }

    private void BezierPath(Vector3 start, Vector3 end)
    {
        end.y = start.y;
                
        float maxAngleLength = Vector3.Distance(start, end)*0.25f;

        Vector3 startAngle = Vector3.MoveTowards(start, end, maxAngleLength);
        startAngle.x += Random.Range(-maxAngleLength, maxAngleLength);
        startAngle.z += Random.Range(-maxAngleLength, maxAngleLength);

        Vector3 endAngle = Vector3.MoveTowards(end, start, maxAngleLength);
        endAngle.x += Random.Range(-maxAngleLength, maxAngleLength);
        endAngle.z += Random.Range(-maxAngleLength, maxAngleLength);
        

        Vector3[] newPath = new Vector3[_pointsCount];

        for (int i = 0; i < _pointsCount; i++)
        {
            float t = (float)i / _pointsCount;
            Vector3 position =
                Mathf.Pow(1 - t, 3) * start +
                3 * Mathf.Pow(1 - t, 2) * t * startAngle +
                3 * (1 - t) * Mathf.Pow(t, 2) * endAngle +
                Mathf.Pow(t, 3) * end;

            newPath[i] = position;
        }
        Path = newPath;

        OnBezierCompleteForDebug?.Invoke(Path, startAngle, endAngle);
    }

    private void StraightPath(Vector3 start, Vector3 end)
    {
        Vector3[] newPath = new Vector3[(int)(Vector3.Distance(start, end) / _distantion) + 1];

        for (int i = 0; i < newPath.Length; i++)
        {
            newPath[i] = Vector3.MoveTowards(start, end, _distantion * (i + 1));
        }
        Path = newPath;
    }
}
