using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;

    public FishNode _fishNode;
    public bool found = false;
    public bool die = false;

    void Update()
    {
        // Check player position
        if(Vector3.Distance(transform.position, _playerTransform.position) < interactionDistance)
        {
            Debug.Log("Player find the fish");
            found = true;
        }
    }

    //栈豆

    public int fishHP = 10;
    private int life;
    public void kill()
    {
        life = fishHP;
        life -= 1;
        if(life <= 0)
        {
            die = true;
        }
    }
}
