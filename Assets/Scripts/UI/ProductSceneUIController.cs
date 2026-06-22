using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProductSceneUIController : MonoBehaviour
{

    [SerializeField] private ProductPortrait portraitView;
    [SerializeField] private ProductLandscape landscapeView;

    private IProductView activeProductView;

    private void Awake()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        bool isLandscape = aspectRatio > 1.3f; 
        portraitView.gameObject.SetActive(!isLandscape);

        landscapeView.gameObject.SetActive(isLandscape);

        activeProductView = isLandscape ? (IProductView)landscapeView : portraitView;
    }
    private void OnEnable()
    {
        activeProductView.OnBackButtonClicked += OnBackButtonClicked;
    }

  
    private void OnDisable()
    {
        activeProductView.OnBackButtonClicked -= OnBackButtonClicked;
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("CatalogueScene");
    }
}
