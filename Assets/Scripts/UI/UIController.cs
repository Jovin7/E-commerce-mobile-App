using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("ViewsPanel")]
    [SerializeField] private PortraitView portraitView;
    [SerializeField] private LandscapeView landscapeView;
    [SerializeField] private ProductCardView productCardPrefab;

    private IHomeView activeHomeView;
    private IDetailView activeDetailView;

    private ProductData selectedProduct;
    private IThumbnailLoaderService thumbnailLoaderService;


    private void Awake()
    {
        bool isLandscape = Screen.width > Screen.height;
        portraitView.gameObject.SetActive(!isLandscape);
        landscapeView.gameObject.SetActive(isLandscape);

        activeHomeView = isLandscape ? (IHomeView)landscapeView : portraitView;
        activeDetailView = isLandscape ? (IDetailView)landscapeView : portraitView;
    }
    private void OnEnable()
    {
        ProductManager.Instance.OnProductsLoaded += PopulateGrid;
        activeDetailView.OnBackButtonRequested += OnBackButtonClicked;
        activeDetailView.OnView3DRequested += OnView3DClicked;
    }

    private void OnDisable()
    {
        ProductManager.Instance.OnProductsLoaded -= PopulateGrid;
        activeDetailView.OnBackButtonRequested -= OnBackButtonClicked;
        activeDetailView.OnView3DRequested -= OnView3DClicked;
    }
    void Start()
    {
        thumbnailLoaderService = new ThumbnailLoaderService();
        if (ProductManager.Instance.SelectedProduct.name != "")
        {
            OnItemSelected(ProductManager.Instance.SelectedProduct);
        }
    }

    private void PopulateGrid(ProductDatabase database)
    {
        foreach (var product in database.products)
        {
            ProductCardView card = Instantiate(productCardPrefab, activeHomeView.GridParent);
            card.Initialize(product, thumbnailLoaderService, OnItemSelected);
            _ = card.LoadThumbnailAsync();
        }
    }

    private async void OnItemSelected(ProductData data)
    {
        selectedProduct = data;
        EnableDetailPanel(true);
        activeDetailView.SetProductInfo(data);
        var sprite = await thumbnailLoaderService.LoadThumbnailAsync(data.thumbnailURL);
        if (sprite != null)
            activeDetailView.SetProductSprite(sprite);


    }
    private void OnView3DClicked()
    {
        ProductManager.Instance.SelectedProduct = selectedProduct;

        SceneManager.LoadScene("Product3DScene");
    }

    private void OnBackButtonClicked()
    {
        EnableDetailPanel(false);

        if (ProductManager.Instance.SelectedProduct != null)
        {
            ProductManager.Instance.SelectedProduct = null;
            PopulateGrid(ProductManager.Instance.data);
        }
    }
    private void EnableDetailPanel(bool v)
    {
        
        activeDetailView.SetDetailActive(v);
        activeHomeView.SetHomeActive(!v);
    }
}
