using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {

    [SerializeField] UnityEvent enterEvent;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            enterEvent.Invoke();
        }
    }

}
