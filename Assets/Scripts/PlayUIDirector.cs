using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayUIDirector : MonoBehaviour {
    // unity ui object's event handler
    public static PlayUIDirector instance;

    public Slider energySlider;
    public GameObject energyCounter;
    public GameObject GameOverUI;
    public Text CoinText;
    public Text ScoreText;

    float powerPercent;
    int powerCount;
    int maxPowerCount;
    GameObject[] energyIconArray;

    // Use this for initialization
    void Start () {
        powerPercent = 1;
        GameOverUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
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

    public void SetPowerSliderValue(float currentPowerPecent) {
        powerPercent = currentPowerPecent;
        energySlider.value = powerPercent;
    }

    public void SetPowerCount(int currentPowerCount) {
        powerCount = maxPowerCount - currentPowerCount;

        // 최대5 현재2라면, 3개를 지워야함
        // 3개는 0,1,2 5-2-1 = 2
        for (int i = 0; i < powerCount; i++) {
            energyIconArray[i].GetComponent<Image>().enabled = false;
        }
        // 그리고 나머지는 보여야함
        // 최대 6개 현재2개라면, 뒤에서부터 2개는 보여야함
        // 6부터 시작이고 4까지만 수행
        // max - current = 4  => 5,4
        for (int i = maxPowerCount - 1; i > powerCount - 1; i--) {
            energyIconArray[i].GetComponent<Image>().enabled = true;
        }

    }

    public void SetCoinValue(int coinCount) {
        CoinText.text = "Coin: " + coinCount;
    }

    public void SetScoreValue(int scoreCount) {
        ScoreText.text = "Score: " + scoreCount;
    }

    public void PopUPGameOver() {
        // save data, score and coin
        GameOverUI.SetActive(true);
    }

    public void SetEnergyUI(int energyType, int maxEnergyCount) {
        maxPowerCount = maxEnergyCount;
        Debug.Log("Set Energy UI " + energyType + " " + maxEnergyCount);

        if (energyType == 0) {
            energySlider.gameObject.SetActive(true);
        } else if (energyType == 1) {
            energyCounter.SetActive(true);
            setEnergyCounter(maxEnergyCount);
        }
    }

    void setEnergyCounter(int maxEnergyCount) {
        energyIconArray = new GameObject[maxEnergyCount];

        // 보정 값
        int correctPosX = 10;
        float leftPosX;
        if (maxEnergyCount % 2 == 0) {
            // 짝수
            // 제일 왼쪽 position 계산
            leftPosX = maxEnergyCount / 2 * correctPosX * -1;
        } else {
            // 홀수
            // 제일 왼쪽 position 계산
            leftPosX = maxEnergyCount / 2 * correctPosX * -1 + (correctPosX/2);
        }

        for (int i = 0; i < maxEnergyCount; i++) {
            GameObject tempEnergyIcon = Instantiate(Resources.Load("Prefabs/EnergyIcon"), energyCounter.transform.position, Quaternion.identity) as GameObject;
            energyIconArray[i] = tempEnergyIcon;
            Vector3 v = new Vector3(leftPosX, 0, 0);

            tempEnergyIcon.transform.parent = energyCounter.transform;

            tempEnergyIcon.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 1.1f, 1f);
            tempEnergyIcon.GetComponent<RectTransform>().localPosition += v;
            leftPosX += correctPosX;
        }
    }
}
