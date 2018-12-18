using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    [Header("Range Settings")]

    [SerializeField] float agroRange;
    [SerializeField] float attackRange;

    [SerializeField] LayerMask ignoreMask;

    [Space(5)]
    [Header("Patrol Settings")]

    [SerializeField] List<Transform> patrolPoints = new List<Transform>();

    enum NPCState {ATTACK, PATROL};

    NPCState currentState;

    NPCCombat combat;
    NPCStats stats;

    public Animator anim;

    public bool dead;
    
    NavMeshAgent navAgent;

    int currentPatrolPoint;

    float attackStopDist;

    void Start () {
        combat = GetComponent<NPCCombat>();
        stats = GetComponent<NPCStats>();

        navAgent = GetComponent<NavMeshAgent>();

        currentState = NPCState.PATROL;

        currentPatrolPoint = 0;
        attackStopDist = navAgent.stoppingDistance;
        //navAgent.stoppingDistance = attackRange / 2;
    }
	
	void Update () {
        RangeScan();

        if (currentState == NPCState.PATROL && patrolPoints.Count > 0 && !dead) {
            Patrol();
            navAgent.stoppingDistance = 2;
        }

        if (anim != null) {
            anim.SetFloat("Forward", navAgent.velocity.z);
            anim.SetFloat("Turn", navAgent.velocity.x);
        }
    }

    void RangeScan() {
        float dist = Vector3.Distance(transform.position, LevelManager.instance.player.transform.position);
        RaycastHit hit;

        Physics.Raycast(transform.position, LevelManager.instance.player.transform.position - transform.position, out hit, attackRange, ignoreMask);

        if (hit.transform != null && hit.transform.gameObject.CompareTag("Player") && !dead) {
            currentState = NPCState.ATTACK;
            navAgent.stoppingDistance = attackStopDist;

            if (anim != null) {
                navAgent.SetDestination(LevelManager.instance.player.transform.position);
            }

            if (dist <= attackRange) {
                transform.LookAt(new Vector3(LevelManager.instance.player.transform.position.x, transform.position.y, LevelManager.instance.player.transform.position.z));

                if (anim == null) {
                    combat.Attack();
                } else {
                    anim.SetTrigger("Shoot");
                }
            } else if (dist <= agroRange && dist > attackRange) {
                navAgent.SetDestination(LevelManager.instance.player.transform.position);
            }
        } else {
            currentState = NPCState.PATROL;
        }
    }

    void Patrol() {
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) <= 2) {
            if (currentPatrolPoint + 1 >= patrolPoints.Count) {
                currentPatrolPoint = 0;
            } else {
                currentPatrolPoint += 1;
            }
        }

        transform.LookAt(new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z));
        navAgent.SetDestination(patrolPoints[currentPatrolPoint].position);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
