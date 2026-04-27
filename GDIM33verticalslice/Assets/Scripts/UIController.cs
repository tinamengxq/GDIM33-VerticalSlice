using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _pamphletContent;
    [SerializeField] private GameObject _pamphletButton;
    [SerializeField] private PipeNode[] pipeNode;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogue;

    void Start()
    {
        _pamphletButton.SetActive(true);
        _pamphletContent.SetActive(false);
        gameController.FindPipe += TellTool;
        gameController.win += WinDia;
        _dialogueUI.SetActive(false);
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

    public void TellTool(int id)
    {
        _dialogueUI.SetActive(true);
        Debug.Log("Show hint");
        _dialogue.text = pipeNode[id].description;
        if (Input.GetKeyDown(KeyCode.F))
        {
            _dialogueUI.SetActive(false);
        }
    }

    void Update()
    {
        if(_dialogueUI && Input.GetKeyDown(KeyCode.F))
        {
            _dialogueUI.SetActive(false);
        }
    }

    public void WinDia()
    {
        _dialogueUI.SetActive(true);
        _dialogue.text = "Ohh I lose. Take your tool!";
        if (Input.GetKeyDown(KeyCode.F))
        {
            _dialogueUI.SetActive(false);
        }
    }
}
