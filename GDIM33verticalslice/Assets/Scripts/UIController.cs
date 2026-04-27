using System;
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
    [SerializeField] private GameObject _hint;
    [SerializeField] private GameObject _bag;

    public event Action<int> CheckTool;

    void Start()
    {
        _pamphletButton.SetActive(true);
        _pamphletContent.SetActive(false);
        gameController.FindPipe += TellTool;
        gameController.win += WinDia;
        _dialogueUI.SetActive(false);
        gameController.BackToPipe += ShowHint;
        _hint.SetActive(false);
        _bag.SetActive(false);
        gameController.problemSolved += End;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowPamphletContent();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnOutside();
        }
        if(_dialogueUI && Input.GetKeyDown(KeyCode.F))
        {
            //NextSentence();
            _dialogueUI.SetActive(false);
        }

        if(interactable == true && Input.GetKeyDown(KeyCode.F))
        {
            _dialogueUI.SetActive(true);
            _hint.SetActive(false);
            _dialogue.text = "Click 1 to hand in tool 1. \nClick 2 to hand in tool 2.\nHint: For milestone1 we only have tool 1.";
        }

        if(_dialogueUI && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            CheckTool?.Invoke(0);
        }
        else if(_dialogueUI && Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckTool?.Invoke(1);
        }
        
    }

    public void WinDia()
    {
        _dialogueUI.SetActive(true);
        _dialogue.text = "Ohh I lose. Take your tool!";
    }

    public bool interactable = false;
    public void ShowHint()
    {
        _hint.SetActive(true);
        interactable = true;
        //_bag.SetActive(true);
    }

    public void End()
    {
        _dialogue.text = "Correct tool! problem is solved";
    }


}
