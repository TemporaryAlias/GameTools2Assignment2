using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectController : MonoBehaviour {

    [SerializeField] GameObject deflectZone;
    [SerializeField] GameObject trail;

    public void EnableDeflect() {
        deflectZone.SetActive(true);
        trail.SetActive(true);
    }

    public void DisableDeflect() {
        deflectZone.SetActive(false);
        trail.SetActive(false);
    }

}
