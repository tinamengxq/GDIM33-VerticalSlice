using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFish : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool newDest = true;
    private Vector3 pos = new Vector3();

    void Update()
    {
        if (newDest == true)
        {
            NewPos();
            navMeshAgent.SetDestination(pos);
            //navMeshAgent.nextPosition = pos;
            newDest = false;
        }
        MoveY();
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
    
    private void MoveY()
    {
        Vector3 posy = transform.position;
        posy.y = Mathf.MoveTowards(posy.y, pos.y, 3 * Time.deltaTime);
        transform.position = posy;
        //navMeshAgent.
    }
}
