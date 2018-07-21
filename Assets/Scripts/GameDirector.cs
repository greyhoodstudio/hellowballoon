using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
    private static GameDirector _instance = null; 

    public static GameDirector Instance
    {
        get
        {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(GameDirector)) as GameDirector;

                if (_instance == null ) {
                    Debug.LogError("There is no active gamedirector class object");
                } 
            }

            return _instance;
        }
    }

    public void LoadPlayScene() {
        SceneManager.LoadScene("PlayScene");
    }

}
