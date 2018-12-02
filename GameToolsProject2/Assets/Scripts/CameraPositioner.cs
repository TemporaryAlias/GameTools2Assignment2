using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour {

    [Header("Camera Settings")]

    [SerializeField] float cameraLerp;
    [SerializeField] Transform cameraPointRegular, cameraPointCombo;

    Transform currentCameraPoint;

    Camera playerCamera;

    void Start() {
        playerCamera = GetComponent<Camera>();

        currentCameraPoint = cameraPointRegular;
    }

    void Update() {
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, currentCameraPoint.position, cameraLerp);

        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, currentCameraPoint.rotation, cameraLerp);
    }

    public void UpdateCameraMode() {
        switch (LevelManager.instance.currentGameState) {

            case LevelManager.GameState.MENU:
                return;

            case LevelManager.GameState.DEFLECT:
                currentCameraPoint = cameraPointRegular;
                return;

            case LevelManager.GameState.COMBO:
                currentCameraPoint = cameraPointCombo;
                return;

        }
    }

}
