/*
 * Store behaviour of the enemy ai
 */

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
        //If the current animation state is death and haspassed = false, update position and disable relevant bools and components
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)") && !hasPassed)
        {
            this.transform.position += new Vector3(0f, 1f, 0f);
            agent.enabled = false;
            canAttack = false;
            canMove = false;
            hasPassed = true;
        }
        //Else if not dead animation
        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Death From Right (1)"))
        {
            //for each projectile, if already removed skip, else if outside of the 10x10 square around user, destroy self
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

            //behave appropriate to bools
            if (!playerInSightRange && !playerInAttackRange && canMove) Patroling();
            if (playerInSightRange && !playerInAttackRange && canAttack) ChasePlayer();
            if (playerInAttackRange && playerInSightRange && canAttack) AttackPlayer();
        }
    }
    /// <summary>
    /// method containing patrol code - walk towards a point then reset walkpoint when reached
    /// </summary>
    private void Patroling()
    {
        //if no walkpoint, set a walkpoint
        if (!walkPointSet) SearchWalkPoint();
        //If yes walkpoint, set destination to walkpoint
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        //Set distance to walk to current - walkpoint
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached, set bool to false
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    /// <summary>
    /// Search for a new walkpoint
    /// </summary>
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //Set the walkpoint to the range + current position
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //If on the ground layer, set to true, else do not change
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    /// <summary>
    /// Set destination to player position
    /// </summary>
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    /// <summary>
    /// Attack the player
    /// </summary>
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //Look at the player
        transform.LookAt(player);
        //If the enemy hasnt already attacked
        if (!alreadyAttacked)
        {
            //Attack code here
            //Generate object
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity);
            go.AddComponent<SphereCollider>();
            if (isBig) go.name = "bullet2";
            go.transform.position += new Vector3(0f, isBig ? 5.0f : 0.8f, 0f);
            go.transform.localRotation = this.transform.localRotation;
            //Add forces
            Rigidbody rb = (go.GetComponent<Rigidbody>() == null) ? go.AddComponent<Rigidbody>() : go.GetComponent<Rigidbody>();
            projectiles.Add(go);
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * -1f, ForceMode.Impulse);
            rb.useGravity = false;
            
            ///End of attack code
            //Set already attacked to true, and reset attack
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    /// <summary>
    /// Set already attacked to false
    /// </summary>
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /// <summary>
    /// Take damage 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    /// <summary>
    /// Destroy the enemy 
    /// </summary>
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Draw gizmos on scene view
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}