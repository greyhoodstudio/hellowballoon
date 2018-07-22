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

    protected void Awake()
    {        
    }

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
        if (Input.GetAxis("Horizontal") > 0) {
            // right
            RightPower();
            StartCoroutine("CoolDown");
            canTouch = false;
        } else if (Input.GetAxis("Horizontal") < 0) {
            // left
            LeftPower();
            StartCoroutine("CoolDown");
            canTouch = false;
        }
    }

    protected virtual void LeftPower() {

    }

    protected virtual void RightPower() {

    }

    protected virtual void Refill() {
        currentPower = maxPower;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOver")
        {
            Debug.Log("Game Over");
            PlayDirector.GameOver();
        }
    }

    protected IEnumerator CoolDown()
    {
        // 쿨다운
        yield return new WaitForSeconds(jumpCoolDown);
        canTouch = true;
    }

    public float CalculatePercentHealth() {
        return currentPower / maxPower;
    }
}
