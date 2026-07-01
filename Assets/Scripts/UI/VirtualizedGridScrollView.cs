using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualizedGridScrollView : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;


    [Header("Layout")]
    private GridLayoutSettings layoutSettings;
    //[SerializeField] private int columns = 2;
    //[SerializeField] private int poolSize = 12;
    //private const float CellWidth = 465f;
    //private const float CellHeight = 465f;
    //private const float SpacingX = 50f;
    //private const float SpacingY = 50f;
    //private const float LeftPadding = 50f;
    //private const float TopPadding = 100f;

    private List<ProductCardView> cardPool = new();


    private ProductDatabase database;
    private ProductCardView prefab;
    private IThumbnailLoaderService thumbnailLoader;
    private Action<ProductData> onItemSelected;

    private int currentStartIndex = -1;

    private float RowHeight => layoutSettings.CellHeight + layoutSettings.SpacingY;
    private float ColumnWidth => layoutSettings.CellWidth + layoutSettings.SpacingX;

    private Transform gridParent;


    private void Awake()
    {
       
    }


    public void Initialize(ProductDatabase database,
                           ProductCardView prefab,
                           Transform GridParent,
                           ScrollRect scrollRect,
                           IThumbnailLoaderService thumbnailLoader,
                           Action<ProductData> onSelected,
                           GridLayoutSettings layoutSettings)
    {
        this.database = database;
        this.prefab = prefab;
        this.thumbnailLoader = thumbnailLoader;
        this.onItemSelected = onSelected;
        this.gridParent = GridParent;
        this.scrollRect = scrollRect;
        this.layoutSettings = layoutSettings;
        this.content = gridParent.GetComponent<RectTransform>();
        CreatePool(database);
        CalculateContentHeight();

        currentStartIndex = -1;
        UpdateVisibleCells();

        scrollRect.onValueChanged.AddListener(_ => UpdateVisibleCells());

    }

    private void OnDisable()
    {
        if(scrollRect!=null)
            scrollRect.onValueChanged.RemoveAllListeners();

    }
    public void CreatePool(ProductDatabase database)
    {
        if (cardPool.Count > 0)
            return;

        for (int i = 0; i < layoutSettings.PoolSize; i++)
        {
            ProductCardView card = Instantiate(prefab, gridParent);
            RectTransform rt = card.GetComponent<RectTransform>();

            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
            cardPool.Add(card);
        }

    }
    private void CalculateContentHeight()
    {
        int totalRows = Mathf.CeilToInt(database.products.Count / (float)layoutSettings.Columns);
        float contentHeight = layoutSettings.TopPadding + totalRows * RowHeight;


        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);

    }
    private void UpdateVisibleCells()
    {
        if (database == null || database.products == null)
            return;

        int topRow = Mathf.Max(0, Mathf.FloorToInt(content.anchoredPosition.y / RowHeight));

        int startIndex = topRow * layoutSettings.Columns;

        if (startIndex == currentStartIndex)
            return;

        currentStartIndex = startIndex;

        RebindPool(startIndex, topRow);
        Debug.Log("UpdateVisibleCells");
    }

    private void RebindPool(int startIndex, int topRow)
    {

        for (int i = 0; i < cardPool.Count; i++)
        {
            int row = topRow + (i / layoutSettings.Columns);
            int col = i % layoutSettings.Columns;

            int productIndex = startIndex + i;
            if (productIndex >= database.products.Count)
            {
                cardPool[i].gameObject.SetActive(false);
                continue;
            }
            cardPool[i].gameObject.SetActive(true);

            RectTransform rt = cardPool[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(layoutSettings.LeftPadding + col * ColumnWidth, -(layoutSettings.TopPadding + row * RowHeight));

         
            cardPool[i].Initialize(database.products[productIndex], thumbnailLoader, onItemSelected);

            _ = cardPool[i].LoadThumbnailAsync();
        }
    }
}

[Serializable]
public class GridLayoutSettings
{
    public int Columns;

    public float CellWidth;
    public float CellHeight;

    public float SpacingX;
    public float SpacingY;

    public float LeftPadding;
    public float TopPadding;

    public int PoolSize;
}