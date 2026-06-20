using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("HomeScreenPanel")]
    [SerializeField] private Transform homeScreenPanel;
    [SerializeField] private Transform gridViewParent;
    [SerializeField] private ProductCardView productCardPrefab;
    [Header("DetailScreenPanel")]
    [SerializeField] private Transform detailScreenPanel;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI category;
    [SerializeField] private TextMeshProUGUI subCategory;
    [SerializeField] private TextMeshProUGUI productDescription;
    [SerializeField] private Image productImage;
    [SerializeField] private Button backButton;

    private IThumbnailLoaderService thumbnailLoaderService;
    
    private void OnEnable()
    {
        ProductManager.Instance.OnProductsLoaded += PopulateGrid;
        backButton.onClick.AddListener(OnBackButtonClicked);

    }

    private void OnBackButtonClicked()
    {
        EnableDetailPanel(false);
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
            card.Initialize(product,thumbnailLoaderService, OnItemSelected);
            
            _ = card.LoadThumbnailAsync();
        }
    }

    private async void OnItemSelected(ProductData data)
    {
        EnableDetailPanel(true);
        productName.text = data.name;
        category.text = data.category;
        subCategory.text = data.subCategory;
        productDescription.text = data.description;
        var sprite = await thumbnailLoaderService.LoadThumbnailAsync(data.thumbnailURL);
        if (sprite != null)
            productImage.sprite = sprite;
    }

    private void EnableDetailPanel(bool v)
    {
        detailScreenPanel.gameObject.SetActive(v);
        homeScreenPanel.gameObject.SetActive(!v);
    }
}
