using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {
    
    private bool touch;
    private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Touch();
	}

    void Touch () {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("ddddd");
            touch = true;
            player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().Jump();
        }
    }
}
