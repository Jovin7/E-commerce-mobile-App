using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance;

    [Header("Services")]
    public IProductDataService productDataService;

    [Header("Data")]
    public ProductDatabase data;

    public event Action<ProductDatabase> OnProductsLoaded;

    public ProductData SelectedProduct;
    public bool isSceneLoaded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
       
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


