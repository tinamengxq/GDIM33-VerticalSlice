using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public static StoryController Instance {get; private set;}
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject _start;
    
    [TextArea(2,4)]
    [SerializeField] private string[] lines;
    private int currentLine = 0;

    void Start()
    {
        _dialogue.SetActive(false);
        _start.SetActive(true);
    }

    void Update()
    {
        if (_dialogue.activeSelf == true && Input.GetKeyDown(KeyCode.F))
        {
            NextLine();
        }
    }

    public void StartClick()
    {
        _start.SetActive(false);
        _dialogue.SetActive(true);
        _dialogueText.text = lines[0];
    }

    public void NextLine()
    {
        currentLine ++;
        if(currentLine >= lines.Length)
        {
            FinishStory();
        }
        ShowLine();
    }

    public void FinishStory()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowLine()
    {
        _dialogueText.text = lines[currentLine];
    }
}
