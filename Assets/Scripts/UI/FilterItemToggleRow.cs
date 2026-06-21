using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilterItemToggleRow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Toggle toggle;

    public void Initialize(ProductData data, bool isOn, Action<bool> onToggled)
    {
        itemNameText.text = data.name;

        toggle.onValueChanged.RemoveAllListeners();
        toggle.isOn = isOn;
        toggle.onValueChanged.AddListener(value => onToggled?.Invoke(value));
    }
}
