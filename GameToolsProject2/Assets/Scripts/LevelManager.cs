﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public UIHandler uiHandler;

    public CameraPositioner playerCamera;

    public GameState currentGameState;

    public AudioSource soundManager;

	void Start ()  {
        SceneManager.sceneLoaded += OnSceneLoad;

        soundManager = GetComponent<AudioSource>();

        //player = FindObjectOfType<PlayerMovement>();
        //playerCamera = FindObjectOfType<CameraPositioner>();
    }
	
	void Update () {

    }

    public void ChangeGameState(GameState newState) {
        currentGameState = newState;
        uiHandler.UIMode(newState);

        switch (currentGameState) {

            case GameState.MENU:
                break;

            case GameState.DEFLECT:
                playerCamera.UpdateCameraMode();
                break;

            case GameState.COMBO:
                playerCamera.UpdateCameraMode();
                break;
            
        }
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(int newSceneIndex) {
        SceneManager.LoadScene(newSceneIndex);
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        player = FindObjectOfType<PlayerMovement>();
        playerCamera = FindObjectOfType<CameraPositioner>();
        uiHandler = FindObjectOfType<UIHandler>();
        soundManager = GetComponent<AudioSource>();

        if (player == null || playerCamera == null) {
            currentGameState = GameState.MENU;
        } else {
            ChangeGameState(GameState.DEFLECT);
        }
    }
}
