using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Character : MonoBehaviour {

    [Header("Jump")]
    [Range(0f, 100f)]
    public float jumpForce = 1.0f;
    [Range(0f, 100f)]
    public float powerForce = 6.0f;
    [Range(0f, 5f)]
    public float jumpCoolDown = 6.0f;

    GameObject[] powers;
    bool canTouch;

    protected void Awake()
    {        
    }

    protected void Start()
    {
        canTouch = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected void FixedUpdate()
    {
        //Rotate(1);
        if (Input.GetAxis("Horizontal") > 0)
        {
            // right
            RightPower();
            Debug.Log("right");
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // left
            LeftPower();
            Debug.Log("left");
        }

    }

    protected void Rotate(float rotateValue) {
        if (gameObject.transform.position.x < 0) {
            gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
        } else if (gameObject.transform.position.x > 0){
            gameObject.GetComponent<Rigidbody2D>().AddTorque(rotateValue);
        }
    }

    protected void LeftPower() {
        //Vector2 jumping = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity;

        //jumping.x = transform.position.x - powerForce;
        //jumping.y = transform.position.y + jumpForce;
        //gameObject.GetComponent<Rigidbody2D>().velocity = jumping;

        gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(new Vector2(-powerForce, jumpForce) * 10f * Time.deltaTime);
    }

    protected void RightPower()
    {
        //Vector2 jumping = gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>().velocity;
        //jumping.x = transform.position.x + powerForce;
        //jumping.y = transform.position.y + jumpForce;
        //gameObject.GetComponent<Rigidbody2D>().velocity = jumping;

        gameObject.transform.GetChild(1).GetComponent<Rigidbody2D>().AddForce(new Vector2(powerForce, jumpForce) * 10f * Time.deltaTime);
    }

    public void Jump()
    {
        canTouch = false;

        StartCoroutine("CoolDown");

        Debug.Log("jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

}
