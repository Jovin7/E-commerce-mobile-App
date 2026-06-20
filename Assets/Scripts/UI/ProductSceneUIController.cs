using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProductSceneUIController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonClicked);

    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("CatalogueScene");
    }
}
