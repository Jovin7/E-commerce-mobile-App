using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductCardView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI productName;

    [SerializeField]
    private TextMeshProUGUI category;

    [SerializeField]
    private TextMeshProUGUI subCategory;

    public void Initialize(ProductData data)
    {
        productName.text = data.name;
        category.text = data.category;
        subCategory.text = data.subCategory;
    }
}
