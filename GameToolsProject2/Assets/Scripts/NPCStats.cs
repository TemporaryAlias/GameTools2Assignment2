using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStats : MonoBehaviour {

    [Header("Enemy Settings")]

    public int maxHP;
    [SerializeField] int hitComboPoints, killComboPoints;

    //DEBUG
    [SerializeField] GameObject targetSprite;

    public bool invuln;

    NPCCombat combat;
    NPCMovement movement;

    public int currentHP;

    NavMeshAgent navAgent;

    void Start() {
        combat = GetComponent<NPCCombat>();
        movement = GetComponent<NPCMovement>();

        currentHP = maxHP;

        navAgent = GetComponent<NavMeshAgent>();

        //DEBUG
        targetSprite.SetActive(false);
    }

    void Update() {
        if (LevelManager.instance.currentGameState == LevelManager.GameState.COMBO) {
            navAgent.ResetPath();

            if (movement.anim != null) {
                movement.anim.SetFloat("Forward", 0);
                movement.anim.SetFloat("Turn", 0);
            }
            
            movement.enabled = false;

            //DEBUG
            targetSprite.SetActive(true);
        } else {
            movement.enabled = true;

            //DEBUG
            targetSprite.SetActive(false);
        }
    }

    public void TakeDamage(int damageDone) {
        if (invuln) {
            return;
        }

        if (currentHP - damageDone > 0)  {
            currentHP -= damageDone;
        } else {
            currentHP = 0;
        }

        if (currentHP <= 0) {
            LevelManager.instance.player.stats.AddCombo(killComboPoints);
            StartCoroutine("Die");
            return;
        }

        LevelManager.instance.player.stats.AddCombo(hitComboPoints);
    }

    public void Explode() {
        //explode effect

        Destroy(gameObject);
    }

    public void Heal(int healAmount) {
        if (currentHP + healAmount > maxHP) {
            currentHP = maxHP;
        } else {
            currentHP += healAmount;
        }
    }

    IEnumerator Die() {
        movement.anim.SetTrigger("Die");
        movement.dead = true;

        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

}
