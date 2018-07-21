using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public string name;
    public float timePlayed;

    public GameData(int scoreInt, string nameStr, float timePlayedF)
    {
        score = scoreInt;
        name = nameStr;
        timePlayed = timePlayedF;
    }
}

public class SaveFile : MonoBehaviour
{
    private static SaveFile _instance = null; 

    public static SaveFile Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(SaveFile)) as SaveFile;

                if (_instance == null)
                {
                    Debug.LogError("There is no active gamedirector class object");
                }
            }

            return _instance;
        }
    }

    private static int currentScore = 0;
    private static string currentName = "Asd";
    private static float currentTimePlayed = 5.0f;

    public static void SetSaveData(int score, string name, float time) {
        currentScore = score;
        currentName = name;
        currentTimePlayed = time;
    }

    public static void SaveData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        GameData data = new GameData(currentScore, currentName, currentTimePlayed);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static GameData LoadData()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        currentScore = data.score;
        currentName = data.name;
        currentTimePlayed = data.timePlayed;

        Debug.Log(data.name);
        Debug.Log(data.score);
        Debug.Log(data.timePlayed);

        return data;
    }
}