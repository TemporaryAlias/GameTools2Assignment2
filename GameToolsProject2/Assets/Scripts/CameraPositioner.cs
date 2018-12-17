using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour {

    [Header("Camera Settings")]

    [SerializeField] float cameraLerp;
    [SerializeField] Transform cameraPointRegular, cameraPointCombo;

    Transform currentCameraPoint;

    Quaternion regularRotation, comboRotation, currentRotation;

    Vector3 posDifference;

    Camera playerCamera;

    void Start() {
        playerCamera = GetComponent<Camera>();

        currentCameraPoint = cameraPointRegular;

        regularRotation = cameraPointRegular.rotation;
        comboRotation = cameraPointCombo.rotation;

        currentRotation = regularRotation;
    }

    void FixedUpdate() {
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, currentCameraPoint.position, cameraLerp);

        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, currentRotation, cameraLerp);
    }

    public void UpdateCameraMode() {
        switch (LevelManager.instance.currentGameState) {

            case LevelManager.GameState.MENU:
                break;

            case LevelManager.GameState.DEFLECT:
                transform.position = LevelManager.instance.player.transform.position + posDifference;
                currentCameraPoint = cameraPointRegular;
                currentRotation = regularRotation;
                break;

            case LevelManager.GameState.COMBO:
                posDifference = LevelManager.instance.player.transform.position - transform.position;
                currentCameraPoint = cameraPointCombo;
                currentRotation = comboRotation;
                break;

        }
    }

}
