using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCharacter : Character {

    protected override void LeftPower() {
        Vector2 v = new Vector2(-directionForce, jumpForce);
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(v, ForceMode2D.Force);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(+rotateValue);
    }

    protected override void RightPower() {
        Vector2 v = new Vector2(+directionForce, jumpForce);
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(v, ForceMode2D.Force);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
    }
}
