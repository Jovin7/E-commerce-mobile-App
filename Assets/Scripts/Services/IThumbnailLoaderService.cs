using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IThumbnailLoaderService
{
    Task<Sprite> LoadThumbnailAsync(string url);
}
