using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Player Settings")]

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject playerModel;

    [Space(5)]
    [Header("Raycast Settings")]
    
    [SerializeField] LayerMask groundMask;

    Camera playerCamera;

	void Start () {
	}
	
	void Update () {
        transform.Translate(playerModel.transform.right * Input.GetAxis("Horizontal") * moveSpeed);
        transform.Translate(playerModel.transform.forward * Input.GetAxis("Vertical") * moveSpeed);

        UpdateRotation();
    }

    void UpdateRotation() {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, groundMask) && hit.collider != null) {
            Vector3 pointToLook = new Vector3(hit.point.x, playerModel.transform.position.y, hit.point.z);

            playerModel.transform.LookAt(pointToLook);
        }
    }

}
