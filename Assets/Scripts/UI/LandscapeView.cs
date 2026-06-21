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

    [Header("DetailScreenPanel")]
    [SerializeField] private Transform detailScreenPanel;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI category;
    [SerializeField] private TextMeshProUGUI subCategory;
    [SerializeField] private TextMeshProUGUI productDescription;
    [SerializeField] private Image productImage;
    [SerializeField] private Button backButton;
    [SerializeField] private Button view3DButton;
    public Transform GridParent => gridViewParent;

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
