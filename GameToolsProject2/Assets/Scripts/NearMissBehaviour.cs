using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMissBehaviour : MonoBehaviour {

    [Header("Near Miss Settings")]

    public int nearMissCombo;

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerStats player = other.GetComponent<PlayerStats>();

            player.AddCombo(nearMissCombo);
        }
    }

}
