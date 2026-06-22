using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductPortrait : MonoBehaviour, IProductView
{
    [SerializeField] private Button backButton;

    public event Action OnBackButtonClicked;


    private void OnEnable()
    {
        backButton.onClick.AddListener(() => OnBackButtonClicked?.Invoke());
    }
    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }
}
