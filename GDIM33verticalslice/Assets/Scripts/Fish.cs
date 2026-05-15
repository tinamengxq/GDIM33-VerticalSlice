using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float interactionDistance;
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private FishNode _fishNode;
    [SerializeField] private MeshRenderer meshRenderer;
    public bool found = false;
    public bool die = false;
    //[SerializeField] private GameObject hurtUI;
    //[SerializeField] private float speed;
    private Vector3 pos = new Vector3();
    private bool newDest = true;
    private float _time = 1f;

    void Start()
    {
        life = _fishNode.HP;
    }
    void Update()
    {
        pos.y = 1.123f;
        // Check player position
        if(Vector3.Distance(transform.position, _playerTransform.position) < interactionDistance)
        {
            Debug.Log("Player find the fish");
            found = true;
            _navMesh.velocity = Vector3.zero;
            _navMesh.isStopped = true;
        }
        else
        {
            found = false;
            _navMesh.isStopped = false;
            if (newDest == true)
            {
                NewPos();
                _navMesh.SetDestination(pos);
                newDest = false;
            }
            
            CheckDest();
        }

        if(life <= 0)
        {
            die = true;
            Debug.Log("Fish die");
           // hurtUI.SetActive(false);
        }

        if (meshRenderer.material.color != Color.white)
        {
            _time -= Time.deltaTime;
            if (_time <= 0f)
            {
                meshRenderer.material.color = Color.white;
                _time = 1f;
            }
        }


    }
    private void NewPos()
    {
        float x = Random.Range(-4.77f,3.07f);
        float y = 1.123f;
        float z = Random.Range(-1.9f,4.72f);
        pos = new Vector3(x, y, z);
    }

    private void CheckDest()
    {
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
        meshRenderer.material.color = Color.red;
    }
}
