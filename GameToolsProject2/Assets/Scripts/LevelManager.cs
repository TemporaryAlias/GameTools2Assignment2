using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //Create an instance of the scene manager so it can be referenced from anywhere
    public static LevelManager instance = null;

    //Generate scene manager, make sure this is the only one. Ensure it isn't removed on scene load
    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //Enum to handle current state of the game
    public enum GameState {MENU, COMBO, DEFLECT};
    
    public PlayerMovement player;

    public CameraPositioner playerCamera;

    public GameState currentGameState;

	void Start () {

	}
	
	void Update () {

    }

    public void ChangeGameState(GameState newState) {
        currentGameState = newState;

        switch (currentGameState) {

            case GameState.MENU:
                break;

            case GameState.DEFLECT:
                break;

            case GameState.COMBO:
                break;
            
        }
        
        playerCamera.UpdateCameraMode();
    }

}
