using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    [SerializeField]
    private Transform gridViewParent;

    [SerializeField]
    private ProductCardView productCardPrefab;

    private IThumbnailLoaderService thumbnailLoaderService;
    
    private void OnEnable()
    {
        ProductManager.Instance.OnProductsLoaded += PopulateGrid;
    }

    private void OnDisable()
    {
        ProductManager.Instance.OnProductsLoaded -= PopulateGrid;
    }
    void Start()
    {
        thumbnailLoaderService = new ThumbnailLoaderService();
    }

    private void PopulateGrid(ProductDatabase database)
    {
        foreach (var product in database.products)
        {
            ProductCardView card = Instantiate(productCardPrefab, gridViewParent);
            card.Initialize(product,thumbnailLoaderService);
            _ = card.LoadThumbnailAsync();
        }
    }
}
