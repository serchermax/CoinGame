using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonForSceneTransitor : MonoBehaviour
{
    [SerializeField] private string _levelName;

    private Button _button;
    private SceneTransitor _sceneTransitor;

    [Inject]
    private void Construct(SceneTransitor sceneTransitor)
    {
        _sceneTransitor = sceneTransitor;
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _sceneTransitor.LoadScene(_levelName);
    }
}
