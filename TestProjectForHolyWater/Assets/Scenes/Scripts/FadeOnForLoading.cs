using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneTransitor))]
public class FadeOnForLoading : FadeOn
{
    private SceneTransitor _sceneTransitor;

    protected override void Awake()
    {
        base.Awake();
        _sceneTransitor = GetComponent<SceneTransitor>();
        _sceneTransitor.OnEndSceneTransite += StartFading;
        _sceneTransitor.OnSceneTransite += OffFade;
    }

    private void OnDestroy()
    {
        _sceneTransitor.OnEndSceneTransite -= StartFading;
        _sceneTransitor.OnSceneTransite -= OffFade;
    }

    private void OffFade()
    {
        foreach (Graphic graphic in _graphicMap)
        {
            Color color = graphic.color;
            color.a = 1f;
            graphic.color = color;
        }
    }

    protected override void AfterFading()
    {
        _sceneTransitor.canvas.enabled = false;
    }
}
