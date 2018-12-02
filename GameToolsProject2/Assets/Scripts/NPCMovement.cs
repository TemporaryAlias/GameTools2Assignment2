using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    [Header("Range Settings")]

    [SerializeField] float agroRange;
    [SerializeField] float attackRange;
    
    NPCCombat combat;
    NPCStats stats;

    NavMeshAgent navAgent;

    void Start () {
        combat = GetComponent<NPCCombat>();
        stats = GetComponent<NPCStats>();

        navAgent = GetComponent<NavMeshAgent>();

        navAgent.stoppingDistance = attackRange / 2;
	}
	
	void Update () {
        RangeScan();
	}

    void RangeScan() {
        float dist = Vector3.Distance(transform.position, LevelManager.instance.player.transform.position);
        
        if (dist <= agroRange && dist > attackRange) {
            navAgent.SetDestination(LevelManager.instance.player.transform.position);
        } else if (dist <= attackRange) {
            transform.LookAt(new Vector3(LevelManager.instance.player.transform.position.x, transform.position.y, LevelManager.instance.player.transform.position.z));
            combat.Attack();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
