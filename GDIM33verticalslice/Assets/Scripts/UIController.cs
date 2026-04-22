using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _pamphletContent;
    [SerializeField] private GameObject _pamphletButton;

    void Start()
    {
        _pamphletButton.SetActive(true);
        _pamphletContent.SetActive(false);
    }

    public void ShowPamphletContent()
    {
        _pamphletContent.SetActive(true);
        _pamphletButton.SetActive(false);
        Debug.Log("Show pamphlet");
    }

    public void ReturnOutside()
    {
        _pamphletContent.SetActive(false);
        _pamphletButton.SetActive(true);
        Debug.Log("Close pamphlet");
    }
}
