using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using System.IO;

public class SceneBuilder : MonoBehaviour
{
    public static bool isDone { get; private set; }

    [SerializeField] private string _assetBundleName;
    
    [Inject] private DiContainer _diContainer;

    public void Start()
    {
        if (TryLoadGameObjectsFromAssetsBundle(out Object[] objects))
        {
            foreach (GameObject gameObject in objects)
            {
                if (gameObject.name != "SceneContext")
                {
                    GameObject temp = _diContainer.InstantiatePrefab(gameObject);
                }
            }
        }
        else Debug.LogError("Произошла ошибка при загрузке Ассет Бандла!");        
    }

    private void OnDestroy()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }

    private bool TryLoadGameObjectsFromAssetsBundle(out Object[] objects)
    {
        objects = null;

        JsonPack jsonPack = JsonUtility.FromJson<JsonPack>
          (File.ReadAllText(Application.persistentDataPath + "/" + JsonManager.JSON_FILE_NAME));

        string path = null;

        foreach(AssetBundleJsonInfo pack in jsonPack.Pack)
        {
            if (pack.AssetBundleName == _assetBundleName)
            {
                path = pack.AssetBundleLocalPath+pack.AssetBundleName;
            }
        }

        if (path != null)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.persistentDataPath + "/" + path);
                        
            if (assetBundle == null) return false;

            objects = assetBundle.LoadAllAssets();

            return true;
        }
        else return false;               
    }
}
