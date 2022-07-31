using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class AssetBundlesOperations
{
    public bool FindBadPacks(JsonPack jsonPack)
    {
        foreach (AssetBundleJsonInfo info in jsonPack.Pack)
        {
            string assetPath = Application.persistentDataPath + "/" + info.AssetBundleLocalPath + info.AssetBundleName;
            string manifestPath = assetPath + ".manifest";

            if (!File.Exists(assetPath) || !File.Exists(manifestPath))
            {                
                Debug.Log("Asset файл не найден!");
                return true;
            }
        }
        Debug.Log("Asset файл найдены!");
        return false;
    }

    public List<AssetBundleJsonInfo> FindBadPacks(JsonPack jsonPack, List<AssetBundleJsonInfo> downloadPack)
    {
        foreach (AssetBundleJsonInfo info in jsonPack.Pack)
        {
            string assetPath = Application.persistentDataPath + "/" + info.AssetBundleLocalPath + info.AssetBundleName;
            string manifestPath = assetPath + ".manifest";

            if (File.Exists(assetPath) && File.Exists(manifestPath))
            {
                Debug.Log("Asset файл найден!");                
            }
            else if (!FindSameInfoInDownloadPack(downloadPack, info))
            {
                downloadPack.Add(info);
                Debug.Log("Asset файл не найден!");
            }     
        }
        return downloadPack;
    }

    private bool FindSameInfoInDownloadPack(List<AssetBundleJsonInfo> downloadPack, AssetBundleJsonInfo checkInfo)
    {
        foreach (AssetBundleJsonInfo info in downloadPack)
            if (checkInfo.AssetBundleName == info.AssetBundleName) return true;

        return false;        
    }

    public async Task<bool> DownloadAssetBundles(List<AssetBundleJsonInfo> downloadPack)
    {
        Task<bool> downloadTask;

        foreach (AssetBundleJsonInfo assetBundleJsonInfo in downloadPack)
        {
            string directory = Application.persistentDataPath + "/" + assetBundleJsonInfo.AssetBundleLocalPath;
            Directory.CreateDirectory(directory);

            directory += assetBundleJsonInfo.AssetBundleName;

            downloadTask = DownloadOperaion.Download(assetBundleJsonInfo.AssetBundleURL, directory);
            await downloadTask;
            if (!downloadTask.Result) return false;

            downloadTask = DownloadOperaion.Download(assetBundleJsonInfo.ManifestURL, directory + ".manifest");
            await downloadTask;
            if (!downloadTask.Result) return false;
        }

        return true;
    }   
}
