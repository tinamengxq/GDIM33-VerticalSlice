using System;
using System.Data.Common;
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
    [SerializeField] private GameObject _backgroundinfo;
    [SerializeField] private TMP_Text _oxygen;
    [SerializeField] private GameObject _dieUI;
    [SerializeField] private GameObject _endUI;

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
        gameController.problemSolved += Solved;
        step1.color = Color.black;
        step2.color = Color.grey;
        //step2.enabled = false;
        step3.color = Color.grey;
        //step3.enabled = false;
        step4.color = Color.grey;
        //step4.enabled = false;
        _backgroundinfo.SetActive(true);
        _oxygen.text = "O2 Level: " + GameController.Instance.oxygenLevel.ToString("F0") + "%";
        _dieUI.SetActive(false);
        gameController.die += Die;
        _endUI.SetActive(false);
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
        step2.color = Color.black;
        step2.enabled = true;
        if (id == 0)
        {
            step2.text = "2. Tool is in fish in red and white";
            _dialogue.text = pipeNode[0].description;
        }
        else if(id == 1)
        {
            step2.text = "2. Tool is in fish in green and white";
            _dialogue.text = pipeNode[1].description;
        }
        
    }

    private bool handing = true;
    private bool handed = false;

    void Update()
    {
        _oxygen.text = "O2 Level: " + GameController.Instance.oxygenLevel.ToString("F0") + "%";

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

        if(interactable == true && Input.GetKeyDown(KeyCode.F) && _hint.activeSelf)
        {
            Debug.Log("interactable == true && Input.GetKeyDown(KeyCode.F) && step == 0");
            _dialogueUI.SetActive(true);
            _hint.SetActive(false);
            _dialogue.text = "Click G to hand in the tool.";
            handing = true;
            return;
        }

        if(_dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.G) && handing == true && _dialogue.text == "Click G to hand in the tool.")
        {
            int id = -1;
            if (step2.text == "2. Tool is in fish in green and white")
            {
                id = 1;
            }
            else if(step2.text == "2. Tool is in fish in red and white")
            {
                id = 0;
            }
            CheckTool?.Invoke(id);
            Debug.Log("Check");
            handing = false;
            handed = true;
            return;
        }
        
        if (end == true && _dialogueUI)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _dialogueUI.SetActive(false);
            }
        }

        if (_dialogueUI.activeSelf && handed == true & Input.GetKeyDown(KeyCode.F) && gameController.oneOK == false)
        {
            _dialogueUI.SetActive(false);
            _endUI.SetActive(true);
            handed = false;
        }
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
    }

    private bool end = false;

    public void Solved()
    {
        _dialogue.text = "Correct tool! The problem of this pipe is solved";
        step4.color = Color.black;
        end = true;
    }

    public void Die()
    {
        _dieUI.SetActive(true);
        Time.timeScale = 0f;
    }

}
