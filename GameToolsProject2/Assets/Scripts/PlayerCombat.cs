using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCombat : MonoBehaviour {

    [SerializeField] GameObject deflectCollider;
    [SerializeField] float deflectCooldown;
    [SerializeField] LayerMask targetableMask;
    [SerializeField] int comboDamage;

    Transform currentTarget;

    bool cooldown;

    PlayerStats stats;

    NavMeshAgent navAgent;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        stats = GetComponent<PlayerStats>();
	}
	
	void Update () {
        if (LevelManager.instance.currentGameState == LevelManager.GameState.DEFLECT) {
            if (currentTarget != null) {
                currentTarget = null;
            }

            transform.rotation = Quaternion.identity;
            navAgent.ResetPath();

            if (Input.GetMouseButtonDown(0) && !cooldown) {
                StartCoroutine("DeflectCooldown");
                stats.anim.SetTrigger("Deflect");
            }
        } else if (LevelManager.instance.currentGameState == LevelManager.GameState.COMBO) {
            if (currentTarget == null) {
                TargetScan();
            } else {
                float dist = Vector3.Distance(transform.position, currentTarget.position);

                Vector3 look = new Vector3(currentTarget.position.x, stats.movement.playerModel.transform.position.y, currentTarget.position.z);
                stats.movement.playerModel.transform.LookAt(look);

                if (dist <= navAgent.stoppingDistance) {
                    navAgent.ResetPath();

                    AttackTarget();
                } else {
                    navAgent.SetDestination(currentTarget.position);
                }
            }
        }
	}

    IEnumerator DeflectCooldown() {
        cooldown = true;

        yield return new WaitForSeconds(deflectCooldown);

        cooldown = false;
    }

    void TargetScan() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetableMask) && hit.collider != null) {
            if (Input.GetMouseButtonDown(0)) {
                currentTarget = hit.collider.transform;
                DashToTarget();
            }
        }
    }

    void AttackTarget() {
        NPCStats targetStats = currentTarget.GetComponent<NPCStats>();

        targetStats.Explode();

        currentTarget = null;
    }

    void DashToTarget() {
        navAgent.SetDestination(currentTarget.position);
    }

}
