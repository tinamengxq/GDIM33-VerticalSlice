using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Dialogue Node", order = 1)]
public class DialogueNode : ScriptableObject
{
    [TextArea(2,4)]
    public string[] Lines;
    public string fishName;
    public List<DialogueOptions> dialogueOptions = new List<DialogueOptions>();
}

[System.Serializable]
public class DialogueOptions
{
    public string optionContent;
    public DialogueNode nextNode;
}
