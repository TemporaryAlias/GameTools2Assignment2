using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    [Header("Player Settings")]

    [SerializeField] int maxHP;
    [SerializeField] int maxCombo;

    public float invulnTime, comboTime;

    public PlayerCombat combat;
    public PlayerMovement movement;

    public Animator anim;

    public bool invuln;

    public bool dead;

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

        UpdateUI();
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

        if (currentHP <= 0 && !dead) {
            StartCoroutine("Die");
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

    IEnumerator Die() {
        anim.SetTrigger("Die");
        dead = true;

        yield return new WaitForSeconds(3);

        LevelManager.instance.uiHandler.StartFadeOut(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI() {
        LevelManager.instance.uiHandler.maxCombo = maxCombo;
        LevelManager.instance.uiHandler.playerMaxHP = maxHP;

        LevelManager.instance.uiHandler.currentCombo = currentCombo;
        LevelManager.instance.uiHandler.playerCurrentHp = currentHP;
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

        anim.SetFloat("Forward", 0);
        anim.SetFloat("Turn", 0);

        invuln = true;
        savedRotation = transform.rotation;

        yield return new WaitForSeconds(comboTime);

        transform.rotation = savedRotation;
        LevelManager.instance.ChangeGameState(LevelManager.GameState.DEFLECT);
        movement.enabled = true;
        invuln = false;
    }

}
