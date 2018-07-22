using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIDirector : MonoBehaviour {

    public GameObject selectPanel;
    //inside class
    bool canSwipe;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    [System.NonSerialized]
    public int charNumber;

    void Awake() {
        charNumber = 0;
        canSwipe = true;
    }

    // Use this for initialization
    void Start () {
        selectPanel = GameObject.Find("SelectPanel");
    }
	
	// Update is called once per frame
	void Update () {
        CastRay();
    }

    // unity ui eventhandler
    public void LoadPlayScene() {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        SceneManager.LoadScene("PlayScene");
    }

    public void CastRay() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null) {
            //Debug.Log(hit.collider.name);
            if (hit.collider.gameObject == selectPanel) {
                CharacterSelect();
            }
        }
    }

    public void CharacterSelect() {
        if (!canSwipe) {
            return;
        }

        int swipeDirection = Swipe();
        // mobile
        //int swipeDirection = Swipe();

        if (swipeDirection == 3) {
            // 부드럽게 왼쪽
            if (charNumber <= 0) {
                charNumber = 2;
            } else {
                charNumber--;
            }
        } else if (swipeDirection == 4) {
            // 부드럽게 오른쪽
            if (charNumber >= 2) {
                charNumber = 0;
            } else {
                charNumber++;
            }
        }

        GameObject.Find("CharNumber").GetComponent<Text>().text = "Number: " + charNumber;
        GameDirector.charNumber = charNumber;
        canSwipe = false;
        StartCoroutine("CoolDown");
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

    public IEnumerator CoolDown() {
        // 쿨다운
        yield return new WaitForSeconds(0.2f);
        canSwipe = true;
    }
}
