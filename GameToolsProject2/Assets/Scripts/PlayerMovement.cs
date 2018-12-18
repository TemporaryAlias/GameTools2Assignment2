using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Player Settings")]

    [SerializeField] float moveSpeed;
    public GameObject playerModel;

    [Space(5)]
    [Header("Raycast Settings")]
    
    [SerializeField] LayerMask groundMask;

    public PlayerStats stats;

    Camera playerCamera;

	void Start () {
        stats = GetComponent<PlayerStats>();
	}
	
	void FixedUpdate () {
        //playerModel. removed for testing

        if (!stats.dead) {
            transform.Translate(playerModel.transform.right * Input.GetAxis("Horizontal") * moveSpeed);
            transform.Translate(playerModel.transform.forward * Input.GetAxis("Vertical") * moveSpeed);
        }
    }

    void Update() {
        if (LevelManager.instance.currentGameState == LevelManager.GameState.DEFLECT) {
            stats.anim.SetFloat("Forward", Input.GetAxis("Vertical"));
            stats.anim.SetFloat("Turn", Input.GetAxis("Horizontal"));
        } else {
            stats.anim.SetFloat("Forward", 0);
            stats.anim.SetFloat("Turn", 0);
        }

        if (!stats.dead) {
            UpdateRotation();
        }
    }

    void UpdateRotation() {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundMask) && hit.collider != null) {
            Vector3 pointToLook = new Vector3(hit.point.x, playerModel.transform.position.y, hit.point.z);

            if (Vector3.Distance(pointToLook, transform.position) > 0.2) {
                playerModel.transform.LookAt(pointToLook);
            }
        }
    }

}
