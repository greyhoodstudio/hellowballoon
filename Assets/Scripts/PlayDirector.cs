using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayDirector : MonoBehaviour {
    
    public static PlayDirector instance;
    public static GameObject restartMenu;
    static int currentCoin;
    static int score;

    [System.NonSerialized]
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

        Debug.Log(GameDirector.charNumber);
        CreateCharacter(GameDirector.charNumber);
    }

	// Update is called once per frame
	void Update () {
		
	}

    void GenerateMapObjects() {
        
    }

    void CreateCharacter(int prefabNum) {
        if (prefabNum == 0) {
            newPlayer = Instantiate(Resources.Load("Prefabs/BasicCharacter")) as GameObject;
        } else if (prefabNum == 1) {
            newPlayer = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        } else if (prefabNum == 2) {
            newPlayer = Instantiate(Resources.Load("Prefabs/ImpulseCharacter")) as GameObject;
        }
        cineMachine.GetComponent<CinemachineVirtualCamera>().Follow = newPlayer.transform as Transform;

        int energyType = newPlayer.GetComponent<Character>().energyType;
        int maxEnergy = newPlayer.GetComponent<Character>().maxEnergy;

        gameObject.GetComponent<PlayUIDirector>().SetEnergyUI(energyType, maxEnergy);
    }

    public void GameOver()
    {
        gameObject.GetComponent<PlayUIDirector>().PopUPGameOver();
    }

    public static void AddCoin() {
        currentCoin++;
    }
}
