using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Projectile Stats")]

    public int damage;

    public float speed, lifetimeInSeconds;

    public bool deflected;

	void Start () {
        StartCoroutine("DestroyTimer");
	}
	
	void Update () {
        transform.Translate(transform.forward * speed);
	}

    IEnumerator DestroyTimer() {
        yield return new WaitForSeconds(lifetimeInSeconds);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (!deflected && other.gameObject.CompareTag("Player")) {
            PlayerStats player = other.GetComponent<PlayerStats>();

            player.TakeDamage(damage);
            Destroy(gameObject);
        } else if (deflected && other.gameObject.CompareTag("Enemy")) {
            NPCStats enemy = other.GetComponent<NPCStats>();

            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
