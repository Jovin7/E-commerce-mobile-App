using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductViewManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ProductDefinition[] productPrefabList;
    private CameraZoom reset;

    void Start()
    {
        ProductManager.Instance.isSceneLoaded = true;
        foreach (var prefab in productPrefabList)
        {
            if (ProductManager.Instance.SelectedProduct.modelCategory.ToLower() == prefab.productName.ToLower())
            {
                Transform model = Instantiate(prefab.modelPrefab.transform, spawnPoint);
                reset = Camera.main.GetComponent<CameraZoom>();
                reset.target = model;
                reset.Initialize();
                return;
            }
           
        }
    }

}
