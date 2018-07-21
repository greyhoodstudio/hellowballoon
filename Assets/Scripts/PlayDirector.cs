using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayDirector : MonoBehaviour {

    public static PlayDirector instance;
    private static GameObject gameOver;

    public int prefabNumber;
    GameObject cineMachine;
    GameObject newPlayer;

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
        prefabNumber = 0;
        cineMachine = GameObject.FindGameObjectWithTag("Cinemachine");
        gameOver = GameObject.FindWithTag("Finish");
        gameOver.SetActive(false);

        if (prefabNumber == 0) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        }
        cineMachine.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform as Transform;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public static void GameOver()
    {
        gameOver.SetActive(true);
    }

}
