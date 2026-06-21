using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LandscapeView : MonoBehaviour, IDetailView, IHomeView
{
    [Header("HomeScreenPanel")]
    [SerializeField] private Transform homeScreenPanel;
    [SerializeField] private Transform gridViewParent;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button filterButton;



    [Header("DetailScreenPanel")]
    [SerializeField] private Transform detailScreenPanel;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI category;
    [SerializeField] private TextMeshProUGUI subCategory;
    [SerializeField] private TextMeshProUGUI productDescription;
    [SerializeField] private Image productImage;
    [SerializeField] private Button backButton;
    [SerializeField] private Button view3DButton;



    [Header("FilterScreenPanel")]
    [SerializeField] private Transform filterScreenPanel;
    [SerializeField] private FilterPanelView filterPanelView;


    [Header("GridLayoutSettings")]
    //[SerializeField] private int columns = 4;
    //[SerializeField] private int poolSize = 20;
    //[SerializeField] private const float CellWidth = 465f;
    //[SerializeField] private const float CellHeight = 465f;
    //[SerializeField] private const float SpacingX = 100f;
    //[SerializeField] private const float SpacingY = 100f;
    //[SerializeField] private const float LeftPadding = 200f;
    //[SerializeField] private const float TopPadding = 100f;
    public GridLayoutSettings laySet;

    public Transform GridParent => gridViewParent;

    public ScrollRect ScrollRect => scrollRect;

    public GridLayoutSettings LayoutSettings => laySet;

    public IFilterView FilterView => filterPanelView;

    public event Action OnBackButtonRequested;
    public event Action OnView3DRequested;
    public event Action<bool> OnFilterButtonClicked;

    private void Awake()
    {
        backButton.onClick.AddListener(() => OnBackButtonRequested?.Invoke());
        view3DButton.onClick.AddListener(() => OnView3DRequested?.Invoke());
        filterButton.onClick.AddListener(() => OnFilterButtonClicked?.Invoke(true));


    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(() => OnBackButtonRequested?.Invoke());
        view3DButton.onClick.RemoveListener(() => OnView3DRequested?.Invoke());
        filterButton.onClick.RemoveListener(() => OnFilterButtonClicked?.Invoke(true));

    }
    public void SetProductInfo(ProductData data)
    {
        productName.text = data.name;
        category.text = data.category;
        subCategory.text = data.subCategory;
        productDescription.text = data.description;
    }

    public void SetProductSprite(Sprite sprite)
    {
        productImage.sprite = sprite;
    }

    public void SetHomeActive(bool isactive)
    {
        homeScreenPanel.gameObject.SetActive(isactive);
    }

    public void SetDetailActive(bool isactive)
    {
        detailScreenPanel.gameObject.SetActive(isactive);
    }

    public void SetFilterScreenActive(bool isactive)
    {
        filterScreenPanel.gameObject.SetActive(isactive);
    }
}
