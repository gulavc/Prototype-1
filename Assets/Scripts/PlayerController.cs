using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInputs();
	}

    private void HandleInputs()
    {
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * 20f);
    }

}
