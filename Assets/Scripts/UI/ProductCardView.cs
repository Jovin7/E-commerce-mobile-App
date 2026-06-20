using System;
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
    [SerializeField] private Button onclickButton;
    private ProductData data;
    private Action<ProductData> onSelected;


    private string imageUrl;
    private IThumbnailLoaderService thumbnailLoader;
    private bool imageLoaded;
    
    public void Initialize(ProductData data, IThumbnailLoaderService thumbnailLoader, Action<ProductData> OnSelected)
    {
        this.data = data;
        this.thumbnailLoader = thumbnailLoader;
        this.onSelected = OnSelected;

        this.productName.text = data.name;
        this.category.text = data.category;
        this.subCategory.text = data.subCategory;
        this.imageUrl = data.thumbnailURL;

        onclickButton.onClick.RemoveAllListeners();
        onclickButton.onClick.AddListener(OnButtonClicked);
    }


    private void OnButtonClicked()
    {
        onSelected?.Invoke(data);
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
