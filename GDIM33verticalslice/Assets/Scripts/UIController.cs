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
    [SerializeField] private GameObject _backgroundinfo;
    [SerializeField] private TMP_Text _oxygen;

    public TMP_Text step1;
    public TMP_Text step2;
    public TMP_Text step3;
    public TMP_Text step4;

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
        step1.color = Color.black;
        step2.color = Color.grey;
        //step2.enabled = false;
        step3.color = Color.grey;
        //step3.enabled = false;
        step4.color = Color.grey;
        //step4.enabled = false;
        _backgroundinfo.SetActive(true);
        _oxygen.enabled = true;
        _oxygen.text = $"O2 level: {GameController.Instance.oxygenLevel:F2}%";
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
        step2.color = Color.black;
        step2.enabled = true;
        step2.text = "2. Tool is in fish in red and white";
    }


    void Update()
    {
        if (_backgroundinfo && Input.GetKeyDown(KeyCode.F))
        {
            _backgroundinfo.SetActive(false);
        }
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
        
        if (end == true && _dialogueUI)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _dialogueUI.SetActive(false);
            }
        }

        _oxygen.text = $"O2 level: {GameController.Instance.oxygenLevel:F2}%";
    }

    public void WinDia()
    {
        _dialogueUI.SetActive(true);
        _dialogue.text = "Ohh I lose. Take your tool!";
        step3.color = Color.black;
    }

    public bool interactable = false;
    public void ShowHint()
    {
        _hint.SetActive(true);
        interactable = true;
        //_bag.SetActive(true);
    }

    private bool end = false;
    public void End()
    {
        _dialogue.text = "Correct tool! problem is solved";
        step4.color = Color.black;
        end = true;
    }


}
