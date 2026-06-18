using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ProductManager : MonoBehaviour
{
    public static ProductManager Instance;
    public IProductDataService productDataService;

    public ProductDatabase data;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    async void Start()
    {

        productDataService = new ProductDataService();
        data = await  productDataService.LoadProductFromJSON();

        Debug.Log("Data loaded");
        
    }

   
}
