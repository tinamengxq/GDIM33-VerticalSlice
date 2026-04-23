using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject",menuName = "FishNode", order = 0)]
public class FishNode : ScriptableObject
{
    // Regular
    public float speed;

    // Fight
    public int fishID;
    public int HP;
    public int ATK;

    // Dialogue
    public DialogueNode dialogueNode;
}

