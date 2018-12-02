using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour {

    [Header("Enemy Settings")]

    public int maxHP;

    public bool invuln;

    NPCCombat combat;
    NPCMovement movement;

    int currentHP;

    void Start() {
        combat = GetComponent<NPCCombat>();
        movement = GetComponent<NPCMovement>();

        currentHP = maxHP;
    }

    void Update() {

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
            Die();
        }
    }

    public void Heal(int healAmount) {
        if (currentHP + healAmount > maxHP) {
            currentHP = maxHP;
        } else {
            currentHP += healAmount;
        }
    }

    void Die() {

    }

}
