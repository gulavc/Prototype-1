using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    public GameObject[] wheels;

    float maxSpeed, maxBackSpeed;


    public float DefaultMaxSpeed = 50f;
    public float DefaultMaxBackSpeed = -10f;

    public float dashSpeed = 30f;
    public float dashStrength = 20f;
    private bool isDashing = false;
    private bool isDashDecreasing = false;
    public float boostDecreaseTime = 1.5f;
    private float dashMaxSpeedLeft;

    private float currentSpeed;

    private bool onGround = false;
    public int MAX_JUMPS = 2;
    private int numJumps;

    public GameObject jumpFlames;
    public GameObject dashFlames;

    public float wheelTurnFactor = 10f;

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

        HandleDashReset();

        SpinWheels();
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
                isDashing = true;
                isDashDecreasing = false;
                maxSpeed += dashSpeed - dashMaxSpeedLeft;
                dashFlames.SetActive(true);                
            }
        }
        if (Input.GetButtonUp("Dash"))
        {
            isDashing = false;
            isDashDecreasing = true;
            dashMaxSpeedLeft = dashSpeed;
            dashFlames.SetActive(false);

        }

    }

    //Handles the cap on positive and negative speed
    private void ManageSpeed()
    {
        currentSpeed = rb.velocity.x;

        if (isDashing)
        {
            rb.AddForce(transform.right * (dashStrength + (maxSpeed - currentSpeed)));
        }

        if (currentSpeed > maxSpeed)
        {            
            rb.AddForce(-transform.right * Mathf.Pow(maxSpeed - currentSpeed, 2));
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

    private void HandleDashReset()
    {
        if (isDashDecreasing) {
            float decrease = ((dashSpeed / boostDecreaseTime) * Time.deltaTime);
            dashMaxSpeedLeft -= decrease;
            maxSpeed -= decrease;
            if (dashMaxSpeedLeft <= 0)
            {
                isDashDecreasing = false;
                maxSpeed += (-dashMaxSpeedLeft); //In order to correct for slight over-slowing
            }
        }
    }

    private void SpinWheels()
    {
        foreach(GameObject w in wheels)
        {
            w.transform.Rotate(0, 0, -currentSpeed * wheelTurnFactor * Time.deltaTime);
        }
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
