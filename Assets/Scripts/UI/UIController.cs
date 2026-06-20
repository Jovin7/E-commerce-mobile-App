using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Button view3DButton;
    private ProductData selectedProduct;
    private IThumbnailLoaderService thumbnailLoaderService;
    
    private void OnEnable()
    {
        ProductManager.Instance.OnProductsLoaded += PopulateGrid;
        backButton.onClick.AddListener(OnBackButtonClicked);
        view3DButton.onClick.AddListener(OnView3DClicked);
    }

    private void OnDisable()
    {
        ProductManager.Instance.OnProductsLoaded -= PopulateGrid;
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
            ProductCardView card = Instantiate(productCardPrefab, gridViewParent);
            card.Initialize(product,thumbnailLoaderService, OnItemSelected);
            
            _ = card.LoadThumbnailAsync();
        }
    }

    private async void OnItemSelected(ProductData data)
    {
        selectedProduct = data;
        EnableDetailPanel(true);
        productName.text = data.name;
        category.text = data.category;
        subCategory.text = data.subCategory;
        productDescription.text = data.description;
        var sprite = await thumbnailLoaderService.LoadThumbnailAsync(data.thumbnailURL);
        if (sprite != null)
            productImage.sprite = sprite;
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
        Debug.Log("Erro");
        detailScreenPanel.gameObject.SetActive(v);
        homeScreenPanel.gameObject.SetActive(!v);
    }
}
