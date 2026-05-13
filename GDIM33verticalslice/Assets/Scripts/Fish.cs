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
    //[SerializeField] private float speed;
    private Vector3 pos = new Vector3();
    private bool newDest = true;

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
            _navMesh.velocity = Vector3.zero;
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

        if (newDest == true)
        {
            NewPos();
            _navMesh.SetDestination(pos);
            newDest = false;
        }
        CheckDest();

    }
    private void NewPos()
    {
        float x = Random.Range(-4.77f,3.07f);
        float y = Random.Range(1.25f,5.14f);
        float z = Random.Range(-1.9f,4.72f);
        pos = new Vector3(x, y, z);
        Debug.Log(pos + "pos");
        Debug.Log(transform.position);
    }

    private void CheckDest()
    {
        Debug.Log(Vector3.Distance(pos, transform.position));
        if (Vector3.Distance(pos, transform.position) <= 5f)
        {
            newDest = true;
        }
    }

    //栈豆

    private int life;
    public void kill()
    {
        life -= 1;
    }
}
