using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ProductDataService : IProductDataService
{
    public async Task<ProductDatabase> LoadProductFromJSON()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "products.json");
        using (UnityWebRequest request = UnityWebRequest.Get(path))
        {

            var operationRequest = request.SendWebRequest();

            while (!operationRequest.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<ProductDatabase>(request.downloadHandler.text);

            }


            Debug.LogError(request.error);
            return null;
        }

    }
}
