using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloWall : MonoBehaviour {

    [Header("HoloWall Settings")]

    [SerializeField] GameObject wallObject;
    public int batteryMaxHp, batteryCurrentHp;

    [SerializeField] AudioClip wallDownClip;

    void Start() {
        batteryCurrentHp = batteryMaxHp;

        wallObject.SetActive(true);
    }

    public void TakeDamage(int damage) {
        batteryCurrentHp -= damage;

        if (batteryCurrentHp <= 0) {
            ToggleWall(false);
        }
    }

	public void ToggleWall(bool newWallState) {
        wallObject.SetActive(newWallState);
        LevelManager.instance.soundManager.PlayOneShot(wallDownClip);
    }

}
