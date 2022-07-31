using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Zenject;
using System.Collections.Generic;

public class SceneTransitor : MonoBehaviour
{
    public Canvas canvas { get; private set; }

    public event Action OnSceneTransite;
    public event Action OnEndSceneTransite;

    private const string MAIN_MENU_NAME = "MainMenu";

    [SerializeField] private Text _message;
    [SerializeField] private float _waitingTimeForError = 4f;
    [SerializeField] private float _waitingTimeForOfflineMode = 2f;

    [Inject] private DiContainer diContainer;

    private bool _offlineMode = false;
    private int _defaultCortingLayer;
    private string _nextSceneName;
    private JsonCheckOperation _jsonCheckOperation;
    private AssetBundlesOperations _assetBundlesOperations;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        canvas = GetComponent<Canvas>();

        _jsonCheckOperation = new JsonCheckOperation();
        _assetBundlesOperations = new AssetBundlesOperations();

        _defaultCortingLayer = canvas.sortingOrder;
        canvas.enabled = false;
        _message.enabled = false;
    }

    public void LoadScene(string name)
    {
        _message.enabled = false;
        canvas.enabled = true;
        _nextSceneName = name;
        OnSceneTransite?.Invoke();

        if (_nextSceneName == MAIN_MENU_NAME)
        {
            canvas.sortingOrder = 999;
            LoadScene();
            return;
        }

        canvas.sortingOrder = _defaultCortingLayer;
        if (_offlineMode) {
            LoadScene();
            return;
        }

        LoadNextScene();
    }

    private async void LoadNextScene()
    {
        JsonPack jsonFindLocal = _jsonCheckOperation.FindLocalJson();

        Task<JsonPack> jsonDownloadNew = _jsonCheckOperation.DonwloadNewJson();
        await jsonDownloadNew;

        JsonPack jsonPackOld = jsonFindLocal;
        JsonPack jsonPackNew = jsonDownloadNew.Result;

        if (jsonPackOld != null && jsonPackNew != null)         // Compare bundle versions and check bad files. Download if need.
        {
            _jsonCheckOperation.ChangeJsonFile(jsonPackNew);
            CompareVersionsCheckFiles(jsonPackOld, jsonPackNew);
            return;
        }

        if (jsonPackOld == null && jsonPackNew != null)    // Download all bundles from new JSON.
        {
            _jsonCheckOperation.ChangeJsonFile(jsonPackNew);
            DownloadAssetBundles(jsonPackNew.Pack);
            return;
        }

        if (jsonPackOld != null && jsonPackNew == null)    // Find bad files. If can, switch on offline mode.
        {
            if (_assetBundlesOperations.FindBadPacks(jsonPackOld)) {
                Error();
                return;
            }

            OfflineMode();
            return;
        }

        Error();                                           // No local, no new( Switch on internet, or nikakoy igri.
    }

    private void CompareVersionsCheckFiles(JsonPack jsonPackOld, JsonPack jsonPackNew)
    {
        List<AssetBundleJsonInfo> packForDownload = _jsonCheckOperation.GetOldPacksForDownload(jsonPackOld, jsonPackNew);
        packForDownload = _assetBundlesOperations.FindBadPacks(jsonPackNew, packForDownload);

        if (packForDownload == null) LoadScene();
        else DownloadAssetBundles(packForDownload);
    }

    private async void DownloadAssetBundles(List<AssetBundleJsonInfo> pack)
    {
        Task<bool> assetBundlesDownload = _assetBundlesOperations.DownloadAssetBundles(pack);
        await assetBundlesDownload;
        if (assetBundlesDownload.Result) LoadScene();
        else Error();
    }

    private void OfflineMode()
    {
        _message.enabled = true;
        _message.text = "Íåò ñîåäèíåíèÿ ñ ñåðâåðîì!\n " +
            "Èãðà áóäåò ðàáîòàòü â îôôëàéí ðåæèìå.";

        _offlineMode = true;
        StartCoroutine(WaitAndLoadScene(_waitingTimeForOfflineMode));
    }

    private void Error()
    {
        _message.enabled = true;
        _message.text = "Îøèáêà! Íåò ñîåäèíåíèÿ ñ ñåðâåðîì.\n " +
            "Ïðèëîæåíèþ òðåáóåòñÿ ñêà÷àòü ôàéëû íåîáõîäèìûå äëÿ èãðû. Ïðîâåðüòå ñîåäèíåíèå ñ èíòåðíåòîì.";

        _nextSceneName = "MainMenu";
        StartCoroutine(WaitAndLoadScene(_waitingTimeForError));
    }

    private IEnumerator WaitAndLoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene();
    }

    private async void LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Single);

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));

        OnEndSceneTransite?.Invoke();
    }
}
