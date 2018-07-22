using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayDirector : MonoBehaviour {

    public static PlayDirector instance;
    private static GameObject gameOver;

    public int charType;
    GameObject cineMachine;
    public GameObject newPlayer;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }

        else if (instance != null) {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
    void Start()
    {
        cineMachine = GameObject.FindGameObjectWithTag("Cinemachine");
        gameOver = GameObject.FindWithTag("Finish");
        gameOver.SetActive(false);
        charType = 0;
        CreateCharacter(charType);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void CreateCharacter(int charType) {
        
        if (charType == 0) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        } else if (charType == 1) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        }
        cineMachine.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform as Transform;
    }

    public static void GameOver()
    {
        gameOver.SetActive(true);
    }

}
