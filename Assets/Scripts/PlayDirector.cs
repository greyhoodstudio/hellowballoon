using UnityEngine;

public class PlayDirector : MonoBehaviour {

    public static PlayDirector instance;
    private static GameObject gameOver;

    public int prefabNumber;

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
        gameOver = GameObject.FindWithTag("Finish");
        gameOver.SetActive(false);

        if (prefabNumber == 0) {
            GameObject myItem = Instantiate(Resources.Load("Prefabs/JumpCharacter")) as GameObject;
        }

    }

	// Update is called once per frame
	void Update () {
		
	}

    public static void PopUPGameOver()
    {
        gameOver.SetActive(true);
    }
}
