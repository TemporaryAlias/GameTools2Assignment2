using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Player Settings")]

    [SerializeField] int maxHP;
    [SerializeField] int maxCombo;

    [SerializeField] float invulnTime, comboTime;

    PlayerCombat combat;
    PlayerMovement movement;

    public bool invuln;

    Quaternion savedRotation;

    public int currentHP, currentCombo;

    void Start () {
        combat = GetComponent<PlayerCombat>();
        movement = GetComponent<PlayerMovement>();

        currentHP = maxHP;
        currentCombo = 0;
	}
	
	void Update () {
		if (currentCombo >= maxCombo) {
            if (Input.GetMouseButtonDown(1)) {
                LevelManager.instance.ChangeGameState(LevelManager.GameState.COMBO);
                StartCoroutine("ComboTimer");
                currentCombo = 0;
            }
        }
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

    IEnumerator ComboTimer() {
        movement.enabled = false;
        invuln = true;
        savedRotation = transform.rotation;

        yield return new WaitForSeconds(comboTime);

        transform.rotation = savedRotation;
        LevelManager.instance.ChangeGameState(LevelManager.GameState.DEFLECT);
        movement.enabled = true;
        invuln = false;
    }

}
