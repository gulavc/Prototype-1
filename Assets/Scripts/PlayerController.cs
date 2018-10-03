using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;

    float maxSpeed, maxBackSpeed;


    public float DefaultMaxSpeed = 50f;
    public float DefaultMaxBackSpeed = -10f;

    public float dashSpeed = 30f;
    public float dashStrength = 20f;
    private bool isDashing = false;

    private float currentSpeed;

    private bool onGround = false;
    public int MAX_JUMPS = 2;
    private int numJumps;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        maxSpeed = DefaultMaxSpeed;
        maxBackSpeed = DefaultMaxBackSpeed;
        numJumps = 0;

    }
	
	// Update is called once per frame
	void Update () {
        HandleInputs();

        ManageSpeed();
	}

    //Handles the management of input controls;
    private void HandleInputs()
    {
        if (onGround)
            rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * 20f);
        else
            transform.Rotate(0, 0, 2 * Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump") && (onGround || numJumps < MAX_JUMPS))
        {
            onGround = false;
            ++numJumps;
            rb.AddForce(this.transform.up * 500f);

        }

        if (Input.GetButtonDown("Dash"))
        {
            if (!isDashing)
            {
                Dash(true);
            }
        }
        if (Input.GetButtonUp("Dash"))
        {
            Dash(false);
        }

    }

    //Handles the cap on positive and negative speed
    private void ManageSpeed()
    {
        currentSpeed = rb.velocity.x;

        if (currentSpeed > maxSpeed)
        {
            rb.AddForce(-transform.right * Mathf.Pow((currentSpeed - maxSpeed), 2));
        }
        if (currentSpeed < maxBackSpeed)
        {
            rb.AddForce(transform.right * Mathf.Pow((currentSpeed - maxBackSpeed), 2));
        }
    }


    //Reset jump counter for double jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            onGround = true;
            numJumps = 0;
        }
    }

    private void Dash(bool starting)
    {
        if (starting)
        {
            isDashing = true;
            maxSpeed += dashSpeed;
            StartCoroutine(Dash());
        }
        else
        {
            isDashing = false;
            maxSpeed -= dashSpeed;
            StopCoroutine(Dash());
        }
        

        
    }

    IEnumerator Dash()
    {
        rb.AddForce(Vector2.right * (dashStrength + (maxSpeed - currentSpeed)));
        yield return new WaitForFixedUpdate();
    }

    public float MaxSpeed
    {
        get; set;
    }

    public float MaxBackSpeed
    {
        get; set;
    }

}
