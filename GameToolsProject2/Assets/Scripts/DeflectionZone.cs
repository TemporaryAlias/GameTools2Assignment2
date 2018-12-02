using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectionZone : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Projectile")) {
            Projectile proj = other.gameObject.GetComponent<Projectile>();

            proj.Deflect(transform);
        }
    }

}
