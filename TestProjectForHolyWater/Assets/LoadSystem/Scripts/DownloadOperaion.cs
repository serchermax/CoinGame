using System.Threading.Tasks;
using UnityEngine.Networking;

public class DownloadOperaion 
{
    public static async Task<bool> Download(string url, string path)
    {
        UnityWebRequest wwwRequest = new UnityWebRequest(url);
        wwwRequest.method = UnityWebRequest.kHttpVerbGET;

        DownloadHandlerFile handler = new DownloadHandlerFile(path);

        handler.removeFileOnAbort = true;
        wwwRequest.downloadHandler = handler;

        wwwRequest.timeout = 5;
        wwwRequest.SendWebRequest();

        while (!wwwRequest.isDone)
        {          
            await Task.Yield();
        }

        if (wwwRequest.result == UnityWebRequest.Result.Success) return true;
        else return false;
    }
}
