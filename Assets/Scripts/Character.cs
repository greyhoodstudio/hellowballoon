using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Character : MonoBehaviour {

    [Header("Jump")]
    [Range(0f, 6f)]
    public float jumpForce = 6.0f;
    [Range(0f, 5f)]
    public float jumpCoolDown = 6.0f;

    bool canTouch;

    protected void Start()
    {
        canTouch = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown("space") && canTouch)
        {
            Jump();
        }
    }

    public void Jump()
    {
        Vector2 jumping = gameObject.GetComponent<Rigidbody2D>().velocity;
        jumping.y = transform.position.y + jumpForce;
        //jumping.x = transform.position.x + jumpForce;
        gameObject.GetComponent<Rigidbody2D>().velocity = jumping;
        canTouch = false;

        StartCoroutine("CoolDown");

        Debug.Log("jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameOver")
        {
            Debug.Log("Game Over");
            PlayDirector.PopUPGameOver();
        }
    }

    protected IEnumerator CoolDown()
    {
        // 쿨다운
        yield return new WaitForSeconds(jumpCoolDown);
        canTouch = true;
    }

}
