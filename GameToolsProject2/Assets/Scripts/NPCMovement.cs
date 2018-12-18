using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour {

    [Header("Range Settings")]

    [SerializeField] float agroRange;
    [SerializeField] float attackRange;

    [SerializeField] LayerMask ignoreMask;

    NPCCombat combat;
    NPCStats stats;

    public Animator anim;

    public bool dead;

    NavMeshAgent navAgent;

    void Start () {
        combat = GetComponent<NPCCombat>();
        stats = GetComponent<NPCStats>();

        navAgent = GetComponent<NavMeshAgent>();

        navAgent.stoppingDistance = attackRange / 2;
	}
	
	void Update () {
        RangeScan();

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
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, agroRange);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
