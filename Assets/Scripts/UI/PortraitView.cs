using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortraitView : MonoBehaviour, IDetailView, IHomeView
{
    [Header("HomeScreenPanel")]
    [SerializeField] private Transform homeScreenPanel;
    [SerializeField] private Transform gridViewParent;
    [SerializeField] private ScrollRect scrollRect;


    [Header("DetailScreenPanel")]
    [SerializeField] private Transform detailScreenPanel;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI category;
    [SerializeField] private TextMeshProUGUI subCategory;
    [SerializeField] private TextMeshProUGUI productDescription;
    [SerializeField] private Image productImage;
    [SerializeField] private Button backButton;
    [SerializeField] private Button view3DButton;

    //[Header("GridLayoutSettings")]
    //[SerializeField] private int columns = 2;
    //[SerializeField] private int poolSize = 12;
    //private const float CellWidth = 465f;
    //private const float CellHeight = 465f;
    //private const float SpacingX = 50f;
    //private const float SpacingY = 50f;
    //private const float LeftPadding = 50f;
    //private const float TopPadding = 100f;


    public GridLayoutSettings laySet;
   
    public Transform GridParent => gridViewParent;
    public ScrollRect ScrollRect => scrollRect;

    public GridLayoutSettings LayoutSettings => laySet;

    public event Action OnBackButtonRequested;
    public event Action OnView3DRequested;

    private void Awake()
    {
        backButton.onClick.AddListener(() => OnBackButtonRequested?.Invoke());
        view3DButton.onClick.AddListener(() => OnView3DRequested?.Invoke());
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
}
