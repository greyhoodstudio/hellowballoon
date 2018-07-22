using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharcter : Character {

    protected override void LeftPower() {
        Vector2 v = new Vector2(-directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(v, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().AddTorque(+rotateValue);
    }

    protected override void RightPower() {
        Vector2 v = new Vector2(+directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddForce(v, ForceMode2D.Impulse);
        //gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
    }
}
