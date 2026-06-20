using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ThumbnailLoaderService : IThumbnailLoaderService
{
    private readonly Dictionary<string, Texture2D> cache = new();

    public async Task<Sprite> LoadThumbnailAsync(string url)
    {
        Texture2D texture;

        if (cache.TryGetValue(url, out texture))
        {
            Debug.Log($"Loaded from cache: {url}");
            return CreateSprite(texture);
        }

        using UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to download image: {url}");
            return null;
        }

        texture = DownloadHandlerTexture.GetContent(request);

        cache[url] = texture;

        Debug.Log($"Downloaded and cached: {url}");

        return CreateSprite(texture);
    }

    private Sprite CreateSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));



    }
}
