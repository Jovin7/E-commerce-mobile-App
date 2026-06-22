using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterPanelView : MonoBehaviour, IFilterView
{
    private RectTransform filterPanel;
    [Header("Category")]
    [SerializeField] private ToggleGroup categoryToggleGroup;
    [SerializeField] private Toggle watchesToggle;
    [SerializeField] private Toggle clothesToggle;
    [SerializeField] private Toggle jewelleryToggle;

    [Header("Subcategory")]
    [SerializeField] private GameObject subCategoryGroup;
    [SerializeField] private ToggleGroup subCategoryToggleGroup;
    [SerializeField] private Toggle maleToggle;
    [SerializeField] private Toggle femaleToggle;
    [SerializeField] private Toggle kidsBoyToggle;
    [SerializeField] private Toggle kidsGirlToggle;

    [Header("Item List")]
    [SerializeField] private Transform itemListParent;
    [SerializeField] private FilterItemToggleRow itemRowPrefab;

    [Header("Bottom Bar")]
    [SerializeField] private Button resetButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button applyButton;

    private readonly List<FilterItemToggleRow> spawnedRows = new();

    public event Action<string> OnCategorySelected;
    public event Action<string> OnSubCategorySelected;
    public event Action<string, bool> OnItemToggled;
    public event Action OnResetClicked;
    public event Action OnCloseClicked;
    public event Action OnApplyClicked;

    private void Awake()
    {
        filterPanel = this.GetComponent<RectTransform>();
        watchesToggle.onValueChanged.AddListener(isOn => { if (isOn) OnCategorySelected?.Invoke("Watches"); });
        clothesToggle.onValueChanged.AddListener(isOn => { if (isOn) OnCategorySelected?.Invoke("Clothes"); });
        jewelleryToggle.onValueChanged.AddListener(isOn => { if (isOn) OnCategorySelected?.Invoke("Jewellery"); });

        maleToggle.onValueChanged.AddListener(isOn => { if (isOn) OnSubCategorySelected?.Invoke("Male"); });
        femaleToggle.onValueChanged.AddListener(isOn => { if (isOn) OnSubCategorySelected?.Invoke("Female"); });
        kidsBoyToggle.onValueChanged.AddListener(isOn => { if (isOn) OnSubCategorySelected?.Invoke("Kids-Boy"); });
        kidsGirlToggle.onValueChanged.AddListener(isOn => { if (isOn) OnSubCategorySelected?.Invoke("Kids-Girl"); });

        resetButton.onClick.AddListener(() => OnResetClicked?.Invoke());
        closeButton.onClick.AddListener(() => OnCloseClicked?.Invoke());
        applyButton.onClick.AddListener(() => OnApplyClicked?.Invoke());
    }
    private void OnEnable()
    {
       filterPanel.anchoredPosition = new Vector2(filterPanel.rect.width, 0);

        LeanTween.move(filterPanel, Vector2.zero, 0.5f).setEaseOutCubic();

    }
  
    public void ShowSubCategoryGroup(bool show) => subCategoryGroup.SetActive(show);

    public void DisplayFilteredItems(IReadOnlyList<ProductData> items, FilterState currentState)
    {
        foreach (var row in spawnedRows)
            Destroy(row.gameObject);
        spawnedRows.Clear();

        foreach (var item in items)
        {
            var row = Instantiate(itemRowPrefab, itemListParent);
            bool isOn = currentState.SelectedItemIds.Contains(item.name);
            row.Initialize(item, isOn, isToggledOn => OnItemToggled?.Invoke(item.name, isToggledOn));
            spawnedRows.Add(row);
        }
    }

    public void RefreshSelectionState(FilterState state)
    {
    
        watchesToggle.SetIsOnWithoutNotify(state.Category == "Watches");
        clothesToggle.SetIsOnWithoutNotify(state.Category == "Clothes");
        jewelleryToggle.SetIsOnWithoutNotify(state.Category == "Jewellery");

        maleToggle.SetIsOnWithoutNotify(state.SubCategory == "Male");
        femaleToggle.SetIsOnWithoutNotify(state.SubCategory == "Female");
        kidsBoyToggle.SetIsOnWithoutNotify(state.SubCategory == "Kids-Boy");
        kidsGirlToggle.SetIsOnWithoutNotify(state.SubCategory == "Kids-Girl");

        subCategoryGroup.SetActive(state.HasCategory);
    }

    public void ResetUI()
    {
        foreach (var row in spawnedRows)
            Destroy(row.gameObject);
        spawnedRows.Clear();
        subCategoryGroup.SetActive(false);

        watchesToggle.SetIsOnWithoutNotify(false);
        clothesToggle.SetIsOnWithoutNotify(false);
        jewelleryToggle.SetIsOnWithoutNotify(false);
        maleToggle.SetIsOnWithoutNotify(false);
        femaleToggle.SetIsOnWithoutNotify(false);
        kidsBoyToggle.SetIsOnWithoutNotify(false);
        kidsGirlToggle.SetIsOnWithoutNotify(false);
    }
}
