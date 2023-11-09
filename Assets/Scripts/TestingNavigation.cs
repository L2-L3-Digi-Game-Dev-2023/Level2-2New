using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class TestThing : MonoBehaviour
{
    public NavMeshSurface surface;
    NavMeshData data;
    public NavMeshAgent agent;
    public LayerMask whatIsGround;
    [SerializeField] Transform target;

    float timer;

    Vector3 destination;



    void Start()
    {
        GameObject empGO = new GameObject();
        target = empGO.transform;
        target.position = new Vector3(0, 0, 0);
        data = surface.navMeshData;
        agent.destination = SetRandomDest(data.sourceBounds);
        Debug.Log(data.sourceBounds);
        timer = 0;
    }



    //Update destination every 5 seconds to test
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            agent.destination = SetRandomDest(data.sourceBounds);
            Debug.Log(data.sourceBounds);
            timer = 0;
        }

    }

    Vector3 SetRandomDest(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var z = Random.Range(bounds.min.z, bounds.max.z);
        if (destination == new Vector3(0,0,0)) destination = new Vector3(x, 1, z);
        else
        {
            destination = new Vector3(x, 1, z);
        }
        //    destination = new Vector3(x, 1, z);
        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))

        target.position = destination;
        return destination;
    }
}
