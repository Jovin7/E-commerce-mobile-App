using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private VirtualizedGridScrollView homeScreenScroll;
    [SerializeField] private VirtualizedGridScrollView FilterScreenScroll;


    private IHomeView activeHomeView;
    private IDetailView activeDetailView;
    private IFilterView activeFilterView;


    private ProductData selectedProduct;
    private IThumbnailLoaderService thumbnailLoaderService;

    private readonly FilterState appliedFilterState = new FilterState();
    private readonly FilterState draftFilterState = new FilterState();


    private void Awake()
    {
        bool isLandscape = Screen.width > Screen.height;
        portraitView.gameObject.SetActive(!isLandscape);
        landscapeView.gameObject.SetActive(isLandscape);

        activeHomeView = isLandscape ? (IHomeView)landscapeView : portraitView;
        activeDetailView = isLandscape ? (IDetailView)landscapeView : portraitView;
        activeFilterView = activeHomeView.FilterView;

    }
    private void OnEnable()
    {
        ProductManager.Instance.OnProductsLoaded += PopulateGrid;
        activeDetailView.OnBackButtonRequested += OnBackButtonClicked;
        activeDetailView.OnView3DRequested += OnView3DClicked;
        activeHomeView.OnFilterButtonClicked += OnFilterButtonClicked;
        activeFilterView.OnCategorySelected += OnFilterCategorySelected;
        activeFilterView.OnSubCategorySelected += OnFilterSubCategorySelected;
        activeFilterView.OnItemToggled += OnFilterItemToggled;
        activeFilterView.OnResetClicked += OnFilterResetClicked;
        activeFilterView.OnCloseClicked += OnFilterCloseClicked;
        activeFilterView.OnApplyClicked += OnFilterApplyClicked;

    }



    private void OnDisable()
    {
        ProductManager.Instance.OnProductsLoaded -= PopulateGrid;
        activeDetailView.OnBackButtonRequested -= OnBackButtonClicked;
        activeDetailView.OnView3DRequested -= OnView3DClicked;
        activeHomeView.OnFilterButtonClicked -= OnFilterButtonClicked;
        activeFilterView.OnCategorySelected -= OnFilterCategorySelected;
        activeFilterView.OnSubCategorySelected -= OnFilterSubCategorySelected;
        activeFilterView.OnItemToggled -= OnFilterItemToggled;
        activeFilterView.OnResetClicked -= OnFilterResetClicked;
        activeFilterView.OnCloseClicked -= OnFilterCloseClicked;
        activeFilterView.OnApplyClicked -= OnFilterApplyClicked;

    }
    void Start()
    {
        thumbnailLoaderService = new ThumbnailLoaderService();
        if (ProductManager.Instance.isSceneLoaded)
        {

            OnItemSelected(ProductManager.Instance.SelectedProduct);
        }
    }


    private void PopulateGrid(ProductDatabase database)
    {
        homeScreenScroll.Initialize(database,
                                    productCardPrefab,
                                    activeHomeView.GridParent,
                                    activeHomeView.ScrollRect,
                                    thumbnailLoaderService,
                                    OnItemSelected,
                                    activeHomeView.LayoutSettings);
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

        if (ProductManager.Instance.isSceneLoaded)
        {
            ProductManager.Instance.isSceneLoaded = false;
            ProductManager.Instance.SelectedProduct = null;
            PopulateGrid(ProductManager.Instance.data);
        }
    }
    private void EnableDetailPanel(bool v)
    {

        activeDetailView.SetDetailActive(v);
        activeHomeView.SetHomeActive(!v);
    }
    private void OnFilterButtonClicked(bool v)
    {
        activeHomeView.SetFilterScreenActive(v);
    }

    private void OnFilterButtonClicked()
    {
        draftFilterState.CopyFrom(appliedFilterState);
        activeFilterView.ResetUI();
        activeFilterView.RefreshSelectionState(draftFilterState);
        activeHomeView.SetFilterScreenActive(true);
    }

    private void OnFilterCategorySelected(string category)
    {
        draftFilterState.Category = category;
        draftFilterState.SubCategory = null;
        draftFilterState.SelectedItemIds.Clear();
        activeFilterView.ShowSubCategoryGroup(true);
        activeFilterView.RefreshSelectionState(draftFilterState);
    }

    private void OnFilterSubCategorySelected(string subCategory)
    {
        draftFilterState.SubCategory = subCategory;
        draftFilterState.SelectedItemIds.Clear();

        var matches = ProductManager.Instance.data.products
            .Where(p => p.category == draftFilterState.Category && p.subCategory == subCategory)
            .ToList();

        activeFilterView.DisplayFilteredItems(matches, draftFilterState);
        activeFilterView.RefreshSelectionState(draftFilterState);
    }
    private void OnFilterItemToggled(string itemId, bool isOn)
    {
        if (isOn) draftFilterState.SelectedItemIds.Add(itemId);
        else draftFilterState.SelectedItemIds.Remove(itemId);
    }

    private void OnFilterResetClicked()
    {
        draftFilterState.Reset();
        activeFilterView.ResetUI();
    }

    private void OnFilterCloseClicked()
    {
        activeHomeView.SetFilterScreenActive(false);
    }

    private void OnFilterApplyClicked()
    {
        appliedFilterState.CopyFrom(draftFilterState);

        var filteredProducts = ApplyFilter(ProductManager.Instance.data.products, appliedFilterState);
        var filteredDatabase = new ProductDatabase
        {
            products = filteredProducts.ToList() 
        };

        PopulateGrid(filteredDatabase);
        activeHomeView.SetFilterScreenActive(false);
    }

    private IReadOnlyList<ProductData> ApplyFilter(IReadOnlyList<ProductData> source, FilterState state)
    {
        IEnumerable<ProductData> result = source;

        if (state.HasCategory)
            result = result.Where(p => p.category == state.Category);
        if (state.HasSubCategory)
            result = result.Where(p => p.subCategory == state.SubCategory);
        if (state.SelectedItemIds.Count > 0)
            result = result.Where(p => state.SelectedItemIds.Contains(p.name));

        return result.ToList();
    }
}
