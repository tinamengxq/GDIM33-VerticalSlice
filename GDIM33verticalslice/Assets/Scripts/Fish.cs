using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private FishNode _fishNode;
    public bool found = false;
    public bool die = false;
    [SerializeField] private GameObject hurtUI;

    void Start()
    {
        life = _fishNode.HP;
    }
    void Update()
    {
        // Check player position
        if(Vector3.Distance(transform.position, _playerTransform.position) < interactionDistance)
        {
            Debug.Log("Player find the fish");
            found = true;
        }
        else
        {
            found = false;
        }
        if(life <= 0)
        {
            die = true;
            Debug.Log("Fish die");
            hurtUI.SetActive(false);
        }
    }

    //栈豆

    private int life;
    public void kill()
    {
        life -= 1;
    }
}
