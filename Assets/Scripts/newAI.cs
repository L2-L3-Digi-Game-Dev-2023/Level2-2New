using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class newAI : MonoBehaviour
{
    float[] maxX = new float[2];
    float[] maxZ = new float[2];
    public NavMeshAgent agent;
    public int range;
    bool hasReached;
    Vector3 curTransPos;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    GameObject sphere;
    //States
    public float sightRange, attackRange;
    [SerializeField] bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = this.transform.position;
        AssignMaxRanges();
        GetNewPos();
        curTransPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.destination == new Vector3(0, 0, 0)) GetNewPos();
        curTransPos = this.transform.position;
        Vector3 diff = curTransPos - agent.destination;
        if (diff.magnitude <= 1f) hasReached = true;
        else hasReached = false;
        if (hasReached)
        {
            hasReached = false;
            GetNewPos();
        }
        else if (transform.position == agent.destination)
        {
            hasReached = true;
        }
    }
    Vector3 ValidatePoint(float x, float y, float z)
    {
        x = x > maxX[0] - 2 ? maxX[0] - 2 : x < maxX[1] + 2 ? maxX[1] + 2 : x;
        z = z > maxZ[0] - 2 ? maxZ[0] - 2 : z < maxZ[1] + 2 ? maxZ[1] + 2 : z;
        Vector3 diff = curTransPos - agent.destination;
        Debug.Log(x + "\n" + z);
        return new Vector3(x, y, z);
    }
    void AssignMaxRanges()
    {
        maxX = new float[]{
            curTransPos.x + range,
            curTransPos.x - range
        };
        maxZ = new float[]
        {
            curTransPos.z + range,
            curTransPos.z - range
        };
    }
    void GetNewPos()
    {
        float newX = (float)Random.Range(maxX[0], maxX[1]);
        float newZ = (float)Random.Range(maxZ[0], maxZ[1]);
        Vector3 diff = curTransPos - new Vector3(newX, 1, newZ);
        agent.SetDestination(ValidatePoint((float)Random.Range(maxX[0], maxX[1]), curTransPos.y, (float)Random.Range(maxZ[0], maxZ[1])));
    }
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);



        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(agent.destination, 2f);
        Gizmos.DrawWireCube(transform.position, new Vector3(range, 1f, range));
    }
}
