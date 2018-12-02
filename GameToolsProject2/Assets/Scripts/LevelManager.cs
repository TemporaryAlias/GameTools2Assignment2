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

    public PlayerMovement player;

	void Start () {
        player = FindObjectOfType<PlayerMovement>();
	}
	
	void Update () {
		
	}

}
