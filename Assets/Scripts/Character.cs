using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    [Header("Force")]
    [Range(0f, 100f)]
    public float jumpForce = 1.0f;
    [Range(0f, 100f)]
    public float directionForce = 6.0f;
    [Range(0f, 5f)]
    public float jumpCoolDown = 0.2f;

    [Header("Rotate")]
    [Range(0f, 100f)]
    public float rotateValue = 0.5f;

    [Header("Energe")]
    [Range(0, 200)]
    public int maxEnergy = 100;

    // 0 is slices, 1 is count
    [System.NonSerialized]
    public int energyType = 0;

    protected float currentEnergy;
    protected bool canTouch;

    protected void Awake() {
        SetCharSetting();
    }

    protected void Start()
    {
        canTouch = true;
        currentEnergy = maxEnergy;
    }

    protected void FixedUpdate()
    {
        Power();
    }

    protected virtual void SetCharSetting() {
        energyType = 0;
    }

    protected virtual void Power() {
        //Rotate(1);
        if (currentEnergy < 0.1)
        {
            return;
        }

        if (Input.GetAxis("Horizontal") > 0) {
            // right
            RightPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentEnergy--;
        } else if (Input.GetAxis("Horizontal") < 0) {
            // left
            LeftPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentEnergy--;
        } else {
            return;
        }

        float healthPercent = CalculatePercentHealth();
        GameObject.FindGameObjectWithTag("PlayDirector").GetComponent<PlayUIDirector>().SetPowerSliderValue(healthPercent);
    }

    protected virtual void LeftPower() {
        Vector2 v = new Vector2(-directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(v, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().AddTorque(+rotateValue);
    }

    protected virtual void RightPower() {
        Vector2 v = new Vector2(+directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(v, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
    }

    protected virtual void Refill() {
        currentEnergy = maxEnergy;
    }

    protected float CalculatePercentHealth() {
        return currentEnergy / maxEnergy;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOver") {
            Debug.Log("Game Over");
            GameObject.FindGameObjectWithTag("PlayDirector").GetComponent<PlayDirector>().GameOver();
        } else if (collision.tag == "Coin") {
            PlayDirector.AddCoin();
            Destroy(collision);
        } else if (collision.tag == "Fuel") {
            Refill();
            Destroy(collision);
        } else if (collision.tag == "Boost") {
            Vector2 v = new Vector2(Random.Range(-1, 1), 1);
            gameObject.GetComponent<Rigidbody2D>().AddForce(v, ForceMode2D.Impulse);
        }
    }

    protected IEnumerator CoolDown() {
        // 쿨다운
        yield return new WaitForSeconds(jumpCoolDown);
        canTouch = true;
    }
}
