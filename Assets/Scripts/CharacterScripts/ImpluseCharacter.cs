using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpluseCharacter : Character {

    [Header("Force")]
    [Range(0f, 100f)]
    public float multiplePower = 1.0f;

    protected override void LeftPower() {
        Vector2 v = new Vector2(-directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(v * multiplePower, ForceMode2D.Impulse);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(+rotateValue);
    }

    protected override void RightPower() {
        Vector2 v = new Vector2(+directionForce, jumpForce).normalized;
        gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(v * multiplePower, ForceMode2D.Impulse);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(-rotateValue);
    }
}
