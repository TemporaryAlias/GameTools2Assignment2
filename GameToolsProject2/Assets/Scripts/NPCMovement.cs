using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    [Header("Range Settings")]

    [SerializeField] float agroRange;
    [SerializeField] float attackRange;

    NavMeshAgent navAgent;

    void Start () {
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.stoppingDistance = attackRange;
	}
	
	void Update () {
        RangeScan();
	}

    void RangeScan() {
        float dist = Vector3.Distance(transform.position, LevelManager.instance.player.transform.position);

        if (dist <= agroRange && dist > attackRange) {
            navAgent.SetDestination(LevelManager.instance.player.transform.position);
        } else if (dist <= attackRange) {

        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
