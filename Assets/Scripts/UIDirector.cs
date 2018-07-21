using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDirector : MonoBehaviour {

    // unity ui object's event handler
    public static UIDirector instance;

    //Initializes the game for each level.
    void InitGame() {

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadPlayScene()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
