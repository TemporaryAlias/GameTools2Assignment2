using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

    [SerializeField] NPCCombat combat;

    public void Shoot() {
        combat.Attack();
    }

}
