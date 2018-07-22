using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayDirector : MonoBehaviour {
    
    public static PlayDirector instance;
    public static GameObject restartMenu;
    static int currentCoin;
    static int score;

    public int charType;
    public GameObject newPlayer;


    GameObject cineMachine;

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
        restartMenu = GameObject.FindWithTag("Finish");
        restartMenu.SetActive(false);
        charType = 0;
        CreateCharacter(charType);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMapObjects() {
        
    }

    void CreateCharacter(int prefabNum) {
        if (prefabNum == 0) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        } else if (prefabNum == 1) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        }
        cineMachine.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform as Transform;
    }

    public static void GameOver()
    {
        restartMenu.SetActive(true);
        // save data, score and coin
    }

    public static void AddCoin() {
        currentCoin++;
    }
}
