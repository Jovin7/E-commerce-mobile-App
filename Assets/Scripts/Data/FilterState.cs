using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FilterState
{
    public string Category;
    public string SubCategory;
    public HashSet<string> SelectedItemIds = new();

    public bool HasCategory => !string.IsNullOrEmpty(Category);
    public bool HasSubCategory => !string.IsNullOrEmpty(SubCategory);

    public void CopyFrom(FilterState other)
    {
        Category = other.Category;
        SubCategory = other.SubCategory;
        SelectedItemIds = new HashSet<string>(other.SelectedItemIds);
    }

    public void Reset()
    {
        Category = null;
        SubCategory = null;
        SelectedItemIds.Clear();
    }
}