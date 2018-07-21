using UnityEngine;

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

    public void SaveHighScore(int score, string name, float time) {
        GameData data = SaveFile.LoadData();
        if (data.score < score) {
            SaveFile.SetSaveData(score, name, time);
            SaveFile.SaveData();
        }
    }

}