using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;
    [SerializeField] private GameController gameController;
    [SerializeField] private UIController uIController;
    public int pipeID;
    public bool found = false;
    public bool correctTool1;
    public bool correctTool2;

    void Start()
    {
        uIController.CheckTool += Check;
    }
    void Update()
    {
        // Check player position
        if(Vector3.Distance(transform.position, _playerTransform.position) < interactionDistance)
        {
            Debug.Log("Player find the pipe");
            found = true;
        }
        else
        {
            found = false;
        }
    }

    public void Check(int id)
    {
        if (id == 0)
        {
            correctTool1 = true;
        }
        if (id == 1)
        {
            correctTool2 = true;
        }
        Debug.Log($"correctTool1 = {correctTool1}");
        Debug.Log($"correctTool1 = {correctTool1}");
    }
}
