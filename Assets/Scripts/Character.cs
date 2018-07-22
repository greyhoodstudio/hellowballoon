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
    [Range(0f, 200f)]
    public float maxPower = 100f;

    protected float currentPower;
    protected bool canTouch;

    protected void Start()
    {
        canTouch = true;
        currentPower = maxPower;
    }

    protected void FixedUpdate()
    {
        if (!canTouch) {
            return;
        }
        Power();
    }

    protected virtual void Power() {
        //Rotate(1);
        if (currentPower < 0.1)
        {
            return;
        }

        if (Input.GetAxis("Horizontal") > 0) {
            // right
            RightPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentPower--;
        } else if (Input.GetAxis("Horizontal") < 0) {
            // left
            LeftPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentPower--;
        }

        float healthPercent = CalculatePercentHealth();
        UIDirector.SetPowerSliderValue(healthPercent);
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
        currentPower = maxPower;
    }

    protected IEnumerator CoolDown()
    {
        // 쿨다운
        yield return new WaitForSeconds(jumpCoolDown);
        canTouch = true;
    }

    protected float CalculatePercentHealth() {
        return currentPower / maxPower;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOver") {
            Debug.Log("Game Over");
            PlayDirector.GameOver();
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
}
