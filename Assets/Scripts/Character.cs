using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    [Header("Force")]
    [Range(0f, 10f)]
    public float jumpForce = 1.0f;
    [Range(0f, 10f)]
    public float powerForce = 6.0f;
    [Range(0f, 5f)]
    public float jumpCoolDown = 6.0f;

    [Header("Rotate")]
    [Range(0f, 5f)]
    public float correctRotate = 2.0f;
    [Range(0f, 5f)]
    public float rotateValue = 0.5f;


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
        if (Input.GetKeyDown("space")) {
            Jump();
        }
    }

    protected void FixedUpdate()
    {
        //Rotate(1);
        if (Input.GetAxis("Horizontal") > 0)
        {
            // right
            RightPower();
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // left
            LeftPower();
        }

        Rotate();
    }

    protected void Rotate() {
        if (gameObject.transform.position.x < 0) {
            gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
        } else if (gameObject.transform.position.x > 0){
            gameObject.GetComponent<Rigidbody2D>().AddTorque(rotateValue);
        }
    }

    protected void LeftPower() {
        Vector2 vector = new Vector2(-powerForce, jumpForce);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(correctRotate);
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);

    }

    protected void RightPower()
    {
        Vector2 vector = new Vector2(powerForce, jumpForce);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(-correctRotate);
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);

    }

    public void Jump()
    {
        canTouch = false;

        gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 100, ForceMode2D.Impulse);

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
