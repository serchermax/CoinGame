using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class FadeOn : MonoBehaviour
{
    [SerializeField] protected float _fadeSpeed = 1f;
    [SerializeField] protected float _fadeCount = 1f;

    protected Graphic[] _graphicMap;

    protected virtual void Awake()
    {
        _graphicMap = GetComponentsInChildren<Graphic>();
    }

    protected virtual void StartFading()
    {
        if (_graphicMap != null) StartCoroutine(Fading());
    }

    protected virtual IEnumerator Fading()
    {   
        while (_graphicMap[0].color.a > 0)
        {
            foreach (Graphic graphic in _graphicMap)
            {
                Color color = graphic.color;
                color.a -= _fadeCount / 100f;
                graphic.color = color;
                yield return new WaitForSeconds(_fadeSpeed / 100f);
            }
        }
        AfterFading();
        StopCoroutine(Fading());
    }

    protected abstract void AfterFading();
}
