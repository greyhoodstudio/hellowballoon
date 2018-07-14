﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {
    
    public float jumpForce = 6.0f;
    private GameObject gameOver;

    void Awake()
    {
        gameOver = GameObject.FindWithTag("GameOver");
    }

    // Use this for initialization
    void Start () {
        gameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jump() {
        Vector2 jumping = gameObject.GetComponent<Rigidbody2D>().velocity;
        jumping.y = transform.position.y + jumpForce;
        //jumping.x = transform.position.x + jumpForce;
        gameObject.GetComponent<Rigidbody2D>().velocity = jumping;

        Debug.Log("jump");
    }

    private void PopUPGameOver()
    {
        gameOver.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameController")
        {
            Debug.Log("Game Over");
            PopUPGameOver();
        }
    }
}
