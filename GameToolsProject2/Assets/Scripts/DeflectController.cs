using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectController : MonoBehaviour {

    [SerializeField] GameObject deflectZone;
    [SerializeField] GameObject trail;

    [SerializeField] AudioClip deflectClip;

    public void EnableDeflect() {
        deflectZone.SetActive(true);
        trail.SetActive(true);

        LevelManager.instance.soundManager.PlayOneShot(deflectClip);
    }

    public void DisableDeflect() {
        deflectZone.SetActive(false);
        trail.SetActive(false);
    }

}
