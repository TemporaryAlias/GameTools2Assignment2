using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMissBehaviour : MonoBehaviour {

    [Header("Near Miss Settings")]

    public int nearMissCombo;

    Projectile proj;

    void Start() {
        proj = GetComponentInParent<Projectile>(); 
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player") && LevelManager.instance.currentGameState != LevelManager.GameState.COMBO && !proj.deflected) {
            PlayerStats player = other.GetComponent<PlayerStats>();

            player.AddCombo(nearMissCombo);
            LevelManager.instance.uiHandler.ComboBarAnim();
        }
    }

}
