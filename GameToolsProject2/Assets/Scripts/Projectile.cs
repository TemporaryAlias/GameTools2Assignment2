using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Projectile Stats")]

    public int damage;

    public float speed;

    public bool deflected;

    [SerializeField] int enemyHitCombo, deflectCombo;

	void Start () {
	}
	
	void FixedUpdate () {
        if (LevelManager.instance.currentGameState != LevelManager.GameState.COMBO) {
            transform.Translate(transform.forward * speed, Space.Self);
        }
	}

    void OnTriggerEnter(Collider other) {
        if (!deflected && other.gameObject.CompareTag("Player")) {
            PlayerStats player = other.GetComponent<PlayerStats>();

            player.TakeDamage(damage);
            Destroy(gameObject);
        } else if (deflected && other.gameObject.CompareTag("Enemy")) {
            NPCStats enemy = other.GetComponent<NPCStats>();

            if (!enemy.invuln) {
                enemy.TakeDamage(damage);

                LevelManager.instance.player.stats.AddCombo(enemyHitCombo);
            }

            Destroy(gameObject);
        } else if (other.gameObject.CompareTag("Battery")) {
            HoloWall battery = other.GetComponentInParent<HoloWall>();

            battery.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("ProjectileBoundary")) {
            Destroy(gameObject);
        }
    }

    public void Deflect(Transform deflector) {
        deflected = true;
        LevelManager.instance.player.stats.AddCombo(deflectCombo);

        float rotation = Vector3.SignedAngle(transform.up, deflector.right, Vector3.up);

        transform.Rotate(transform.forward, -rotation * 2, Space.World);
    }

}
