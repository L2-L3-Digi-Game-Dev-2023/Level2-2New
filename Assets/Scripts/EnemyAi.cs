using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public bool isBig;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    bool canAttack = true, canMove = true, hasPassed = false;
    GameObject sphere;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Animator anim;
    List<GameObject> projectiles = new List<GameObject>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        


    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)") && !hasPassed)
        {
            this.transform.position += new Vector3(0f, 1f, 0f);
            agent.enabled = false;
            canAttack = false;
            canMove = false;
            hasPassed = true;
        }
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)"))
        {
            foreach (GameObject go in projectiles.ToList())
            {
                if (go == null) continue;
                if (go.transform.position.x > (this.transform.position.x + 10) || go.transform.position.z > this.transform.position.z + 10 || go.transform.position.x < (this.transform.position.x - 10) || go.transform.position.z < this.transform.position.z - 10)
                {
                    Destroy(go);
                    projectiles.Remove(go);
                }

            }
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange && canMove) Patroling();
            if (playerInSightRange && !playerInAttackRange && canAttack) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && canAttack) AttackPlayer();
        }
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
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

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
            go.AddComponent<SphereCollider>();
            if (isBig) go.name = "bullet2";
            go.transform.position += new Vector3(0f, isBig ? 5.0f : 0.8f, 0f);
            go.transform.localRotation = this.transform.localRotation;
            Rigidbody rb = (go.GetComponent<Rigidbody>() == null) ? go.AddComponent<Rigidbody>() : go.GetComponent<Rigidbody>();
            projectiles.Add(go);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * -1f, ForceMode.Impulse);
            rb.useGravity = false;
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