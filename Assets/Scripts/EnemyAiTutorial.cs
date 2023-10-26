using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    GameObject sphere;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    List<GameObject> projectiles = new List<GameObject>();

    private void Awake()
    {
        player = GameObject.Find("Player Controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        

    }
    private void Update()
    {
        foreach(GameObject go in projectiles.ToList()){
            if (go.transform.position.x>(this.transform.position.x+10) || go.transform.position.z > this.transform.position.z + 10 || go.transform.position.x<(this.transform.position.x-10) || go.transform.position.z < this.transform.position.z - 10) {
                Destroy(go);
                projectiles.Remove(go);
            }

        }
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void LateUpdate(){
        this.sphere.transform.position=walkPoint;
        Debug.Log($"{sphere.transform.position.x}, {sphere.transform.position.y}, {sphere.transform.position.z}");
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        Debug.Log("Searching walkpoint");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        Debug.Log($"Walkpoint select {randomZ},{randomX}");
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity);
            go.transform.localRotation = this.transform.localRotation;
            Rigidbody rb = (go.GetComponent<Rigidbody>() == null) ? go.AddComponent<Rigidbody>() : go.GetComponent<Rigidbody>();
            projectiles.Add(go);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void RemoveProjectile()
    {
        /*if(projectile.transform.position.x>(this.transform.position.x+10))
        {
           Destroy(projectile);
        }*/
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
