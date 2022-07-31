using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

public class JsonCheckOperation
{
    public const string JSON_FILE_URL = "https://drive.google.com/uc?export=download&id=14esbfK3dAvsnLiVcrdmcxc_VHx5__YZn";

    public JsonPack FindLocalJson()
    {
        JsonPack jsonPack;
        string jsonPath = Application.persistentDataPath + "/" + JsonManager.JSON_FILE_NAME;

        try
        {
            jsonPack = JsonUtility.FromJson<JsonPack>(File.ReadAllText(jsonPath));
            Debug.Log("Json файл найден!");
        }
        catch
        {
            Debug.Log("Json файл не найден!");
            jsonPack = null;
        }
        return jsonPack;
    }

    public async Task<JsonPack> DonwloadNewJson()
    {
        JsonPack jsonPack;
        string jsonPath = Application.persistentDataPath + "/" + "Temp" + JsonManager.JSON_FILE_NAME;

        Task<bool> downloadTask = DownloadOperaion.Download(JSON_FILE_URL, jsonPath);
        await downloadTask;

        if (downloadTask.Result)
        {
            jsonPack = JsonUtility.FromJson<JsonPack>(File.ReadAllText(jsonPath));
        }
        else jsonPack = null;

        return jsonPack;
    }

    public List<AssetBundleJsonInfo> GetOldPacksForDownload(JsonPack jsonPackOld, JsonPack jsonPackNew)
    {
        List<AssetBundleJsonInfo> temp = new List<AssetBundleJsonInfo>();

        for (int i = 0; i < jsonPackNew.Pack.Count; i++)
        {
            if (jsonPackOld.Pack.Count > i)
            {
                if (jsonPackNew.Pack[i].version > jsonPackOld.Pack[i].version)
                    temp.Add(jsonPackNew.Pack[i]);
            }
            else temp.Add(jsonPackNew.Pack[i]);
        }

        return temp;
    }

    public void ChangeJsonFile(JsonPack jsonPackNew)
    {
        File.WriteAllText(Application.persistentDataPath + "/" + JsonManager.JSON_FILE_NAME, JsonUtility.ToJson(jsonPackNew, true));
        File.Delete(Application.persistentDataPath + "/" + "Temp" + JsonManager.JSON_FILE_NAME);
    }
}
