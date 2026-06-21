using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFilterView
{
    event Action<string> OnCategorySelected;
    event Action<string> OnSubCategorySelected;
    event Action<string, bool> OnItemToggled;
    event Action OnResetClicked;
    event Action OnCloseClicked;
    event Action OnApplyClicked;

    void ShowSubCategoryGroup(bool show);
    void DisplayFilteredItems(IReadOnlyList<ProductData> items, FilterState currentState);
    void RefreshSelectionState(FilterState state);
    void ResetUI();
}