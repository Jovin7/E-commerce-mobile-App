using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance;

    [Header("Services")]
    public IProductDataService productDataService;

    [Header("Data")]
    public ProductDatabase data;

    public event Action<ProductDatabase> OnProductsLoaded;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
       
        DontDestroyOnLoad(this.gameObject);
    }
    private async void Start()
    {

        productDataService = new ProductDataService();

        data = await productDataService.LoadProductFromJSON();

        Debug.Log("Data loaded");
        OnProductsLoaded?.Invoke(data);


    }


}
