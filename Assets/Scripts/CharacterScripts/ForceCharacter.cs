using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCharacter : Character {

    protected override void Power() {
        //Rotate(1);
        if (currentPower < 0) {
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
    }

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
