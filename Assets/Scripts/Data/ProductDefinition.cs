using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Products/Product")]
public class ProductDefinition : ScriptableObject
{
    public string productName;
    public GameObject modelPrefab;
}
