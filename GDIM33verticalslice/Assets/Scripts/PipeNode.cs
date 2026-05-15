using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "Pipe Node", order = 2)]
public class PipeNode : ScriptableObject
{
    public int toolID;
    [TextArea(2,4)]
    public string description;
}
