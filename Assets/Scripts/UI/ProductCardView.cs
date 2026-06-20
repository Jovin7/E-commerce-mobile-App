using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI category;
    [SerializeField] private TextMeshProUGUI subCategory;
    [SerializeField] private Image thumbnailImage;

    private string imageUrl;
    private IThumbnailLoaderService thumbnailLoader;
    private bool imageLoaded;

    public void Initialize(ProductData data, IThumbnailLoaderService thumbnailLoader)
    {
        this.productName.text = data.name;
        this.category.text = data.category;
        this.subCategory.text = data.subCategory;
        this.thumbnailLoader = thumbnailLoader;
        this.imageUrl = data.thumbnailURL;
    }

    public async Task LoadThumbnailAsync()
    {
        if (imageLoaded )
            return;

        imageLoaded = true;
        var sprite = await thumbnailLoader.LoadThumbnailAsync(imageUrl);
        if (sprite != null) thumbnailImage.sprite = sprite;

    }
}
