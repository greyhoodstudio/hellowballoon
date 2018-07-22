using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpluseCharacter : Character {

    [Header("Force")]
    [Range(0f, 100f)]
    public float multiplePower = 1.0f;

    protected override void SetCharSetting() {
        energyType = 1;
    }

    protected override void Power() {
        //Rotate(1);
        if (currentEnergy < 0.1 || !canTouch) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            // right
            RightPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentEnergy--;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            // left
            LeftPower();
            StartCoroutine("CoolDown");
            canTouch = false;
            currentEnergy--;
        }

        GameObject.FindGameObjectWithTag("PlayDirector").GetComponent<PlayUIDirector>().SetPowerCount((int)currentEnergy);
    }

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
