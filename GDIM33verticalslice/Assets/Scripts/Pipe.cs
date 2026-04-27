using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;
    [SerializeField] private GameController gameController;
    [SerializeField] private UIController uIController;
    public int pipeID;
    public bool found = false;
    public bool correctTool;

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

    public void Check(int i)
    {
        if (i == pipeID)
        {
            correctTool = true;
        }
        else
        {
            Debug.Log("Incorrect tool!");
        }
    }
}
