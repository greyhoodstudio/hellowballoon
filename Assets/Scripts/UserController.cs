using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {
    
    private bool touch;
    private GameObject player;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

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
            touch = true;
            player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().Jump();
        }
    }
}
