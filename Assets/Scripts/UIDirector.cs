using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour {

    // unity ui object's event handler
    public static UIDirector instance;

    public Slider powerSlider;
    public GameObject powerCounter;

    static float powerPercent;
    public static int powerCount;

    GameObject selectPanel;
    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start() {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "MainScene") {
            selectPanel = GameObject.Find("SelectPanel");
        } else if (SceneManager.GetActiveScene().name == "PlayScene") {
            powerPercent = 1;
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "MainScene") {
            CastRay();
        } else if (SceneManager.GetActiveScene().name == "PlayScene") {
            powerSlider.value = powerPercent;
        }
    }

    // unity ui eventhandler
    public void LoadPlayScene() {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene("PlayScene");
    }

    // unity ui eventhandler
    public void LoadMainScene() {
        SceneManager.LoadScene("MainScene");
    }

    public static void SetPowerSliderValue(float currentPowerPecent) {
        Debug.Log(currentPowerPecent);
        powerPercent = currentPowerPecent;
    }

    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject == selectPanel)
            {
                CharacterSelect();
            }
        }
    }

    public void CharacterSelect()
    {
        int swipeDirection = Swipe();
        // mobile
        //int swipeDirection = Swipe();

        if (swipeDirection == 3)
        {
            // 부드럽게 왼쪽
        }
        else if (swipeDirection == 4)
        {
            // 부드럽게 오른쪽
        }
    }

    public int Swipe() {

        if (Input.GetMouseButtonDown(0)) {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            return 0;
        }

        if (Input.GetMouseButton(0)) {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                Debug.Log("up swipe");
                return 1;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                Debug.Log("down swipe");
                return 2;
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                Debug.Log("left swipe");
                return 3;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                Debug.Log("right swipe");
                return 4;
            }
        }

        return 0;
    }

    public int TouchSwipe() {
        if (Input.touches.Length > 0) {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
                return 0;
            }
            if (t.phase == TouchPhase.Moved) {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    Debug.Log("up swipe");
                    return 1;
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    Debug.Log("down swipe");
                    return 2;
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    Debug.Log("left swipe");
                    return 3;
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    Debug.Log("right swipe");
                    return 4;
                }
            }
        }
        return 0;
    }
}
