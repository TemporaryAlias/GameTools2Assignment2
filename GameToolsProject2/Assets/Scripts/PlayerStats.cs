using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Player Settings")]

    public int maxHP;
    public int maxCombo;
    
    public bool invuln;

    int currentHP, currentCombo;

    void Start () {
        currentHP = maxHP;
        currentCombo = 0;
	}
	
	void Update () {
		
	}

    public void TakeDamage(int damageDone) {
        if (invuln) {
            return;
        }

        if (currentHP - damageDone > 0) {
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

    public void AddCombo(int comboToAdd) {
        if (currentCombo + comboToAdd > maxCombo) {
            currentCombo = maxCombo;
        } else {
            currentCombo += comboToAdd;
        }
    }

    void Die() {

    }

}
