using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCombat : MonoBehaviour {

    [Header("Projectile Settings")]

    [SerializeField] Transform projectileFirePoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int projectileDamage, projectileNearMissCombo;
    [SerializeField] float projectileSpeed;
    [SerializeField] float attackCooldown;

    [SerializeField] AudioClip shootClip;

    NPCMovement movement;
    NPCStats stats;

    bool attackReady;

    void Start () {
        movement = GetComponent<NPCMovement>();
        stats = GetComponent<NPCStats>();

        attackReady = true;
	}
	
	void Update () {
		
	}

    public void Attack() {
        if (attackReady) {
            GameObject newProjectile = Instantiate(projectilePrefab, projectileFirePoint.position, projectileFirePoint.rotation);
            LevelManager.instance.soundManager.PlayOneShot(shootClip);

            Projectile projectile = newProjectile.GetComponent<Projectile>();
            NearMissBehaviour nearMiss = newProjectile.GetComponentInChildren<NearMissBehaviour>();

            projectile.damage = projectileDamage;
            projectile.speed = projectileSpeed;

            nearMiss.nearMissCombo = projectileNearMissCombo;

            StartCoroutine("AttackCooldown");
        }
    }

    IEnumerator AttackCooldown() {
        attackReady = false;

        yield return new WaitForSeconds(attackCooldown);

        attackReady = true;
    }

}
