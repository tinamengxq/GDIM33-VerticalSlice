using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public static StoryController Instance {get; private set;}
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private string[] lines;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        _dialogue.SetActive(true);
        _dialogueText.text = lines[0];
    }

    void Update()
    {
        
    }
}
