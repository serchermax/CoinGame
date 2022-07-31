using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "JsonManager", menuName = "ScriptableObjects/JsonManager", order = 1)]
public class JsonManager : ScriptableObject
{
    public const string JSON_FILE_NAME = "AssetsBundleInfo.json";

    [TextArea()]
    [SerializeField] private string jsonFilePath;
    [SerializeField] private AssetBundleJsonInfo assetBundleJsonInfo;

    [ContextMenu("CreateLocalFile")]
    private void CreateLocalFile()
    {
        JsonPack jsonPack;
        try
        {
            jsonPack = JsonUtility.FromJson<JsonPack>(File.ReadAllText(Application.dataPath + jsonFilePath + JSON_FILE_NAME));
        }
        catch
        {
            jsonPack = new JsonPack(new List<AssetBundleJsonInfo>());
        }

        if (TryFindBundleIndexByName(jsonPack, assetBundleJsonInfo.AssetBundleName, out int index))
        {
            jsonPack.Pack[index] = assetBundleJsonInfo;
        }
        else jsonPack.Pack.Add(assetBundleJsonInfo);

        File.WriteAllText(Application.dataPath + jsonFilePath + JSON_FILE_NAME, JsonUtility.ToJson(jsonPack, true));        
    }

    [ContextMenu("DeleteBundleByName")]
    private void DeleteLocalFile()
    {
        JsonPack jsonPack;
        try
        {
            jsonPack = JsonUtility.FromJson<JsonPack>(File.ReadAllText(Application.dataPath + jsonFilePath + JSON_FILE_NAME));
        }
        catch
        {
            Debug.LogWarning(this + ": Файл не найден!");
            return;
        }

        if (TryFindBundleIndexByName(jsonPack, assetBundleJsonInfo.AssetBundleName, out int index))
        {
            jsonPack.Pack.RemoveAt(index);
        }       

        File.WriteAllText(Application.dataPath + jsonFilePath + JSON_FILE_NAME, JsonUtility.ToJson(jsonPack, true));
    }


    private bool TryFindBundleIndexByName(JsonPack jsonPack, string bundleName, out int index)
    {
        for (int i = 0; i < jsonPack.Pack.Count; i++)
        {
            if (jsonPack.Pack[i].AssetBundleName == bundleName)
            {
                index = i;
                return true;
            }
        }
        index = 0;
        return false;
    }
}

public class JsonPack
{
    public JsonPack(List<AssetBundleJsonInfo> newPack)
    {
        Pack = newPack;
    }

    public List<AssetBundleJsonInfo> Pack;
}

[System.Serializable]
public class AssetBundleJsonInfo
{
    public string AssetBundleName;
    public int version;
    public string AssetBundleURL;
    public string ManifestURL;
    [TextArea()]
    public string AssetBundleLocalPath;
}