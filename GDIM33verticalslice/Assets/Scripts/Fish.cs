using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;

    public FishNode _fishNode;

    void Update()
    {
        // Check player position
        if(Vector3.Distance(transform.position, _playerTransform.position) < interactionDistance)
        {
            Debug.Log("Player find the pipe");
        }
    }
}
