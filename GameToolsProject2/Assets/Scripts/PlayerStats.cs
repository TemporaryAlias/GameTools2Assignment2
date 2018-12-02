using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Player Settings")]

    [SerializeField] int maxHP;
    [SerializeField] int maxCombo;

    [SerializeField] float invulnTime;

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

        StartCoroutine("InvulnTimer");
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

    IEnumerator InvulnTimer() {
        invuln = true;

        yield return new WaitForSeconds(invulnTime);

        if (LevelManager.instance.currentGameState != LevelManager.GameState.COMBO) {
            invuln = false;
        }
    }

}
