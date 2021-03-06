﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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

    public float maxBoost = 4f; //total time before boost is depleted, in seconds
    private float currentBoost;

    private float currentSpeed;

    private bool onGround = false;
    public int MAX_JUMPS = 2;
    private int numJumps;

    public GameObject dashFlames;

    public ParticleSystem smoke;
    private ParticleSystem.EmissionModule smokeEmitter;
    private ParticleSystem.Particle smokeParticle;
    public float smokeMinRate = 0.5f;
    public float smokeMaxRate = 5f;

    public float wheelTurnFactor = 10f;

    private AnimationManager animator;

    private bool canBackflip;

    private bool doJump = false;
    private float horizontalInput;

    public bool IsImmuneToDamage { get; set; } = false;

    private float currentRotationVelocity = 0;

    // Use this for initialization
    void Start()
    {
        //Time.maximumDeltaTime = 0.03f;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        maxSpeed = DefaultMaxSpeed;
        maxBackSpeed = DefaultMaxBackSpeed;
        numJumps = 0;
        canBackflip = false;
        currentBoost = maxBoost;

        smokeEmitter = smoke.emission;
        //smokeParticle = smoke.Par
        animator = GetComponent<AnimationManager>();
    }

    void Update()
    {
        HandleInputs();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageSpeed();

        HandleJump();

        HandleDashReset();

        SpinWheels();

        ManageSmoke();

        DampenRotation();
    }

    //Handles the management of input controls;
    private void HandleInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (!onGround)
        {

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && canBackflip)
            {
                canBackflip = false;
                animator.Backflip();
            }
        }

        if (Input.GetButtonDown("Jump") && (onGround || numJumps < MAX_JUMPS))
        {
            doJump = true;

        }

        if (Input.GetButtonDown("Dash"))
        {
            if (!isDashing && currentBoost > 0)
            {
                Dash(true);
            }
        }
        if (Input.GetButtonUp("Dash"))
        {
            if (currentBoost > 0 && isDashing)
            {
                Dash(false);
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            ResetPosition(Vector2.up * 5f);
        }

    }

    //Handles the cap on positive and negative speed
    private void ManageSpeed()
    {
        currentSpeed = rb.velocity.x;

        rb.AddForce(Vector2.right * horizontalInput * 20f);

        if (isDashing)
        {
            rb.AddForce(Vector3.right * (dashStrength + (maxSpeed - currentSpeed)) * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            currentBoost -= Time.fixedDeltaTime;
            if (currentBoost <= 0f)
            {
                Dash(false);
            }
        }

        if (currentSpeed > maxSpeed)
        {
            rb.AddForce(-Vector3.right * Mathf.Pow(maxSpeed - currentSpeed, 2));
        }
        if (currentSpeed < maxBackSpeed)
        {
            rb.AddForce(Vector3.right * Mathf.Pow((currentSpeed - maxBackSpeed), 2));
        }


    }

    private void HandleJump()
    {
        if (doJump)
        {
            //onGround = false;
            ++numJumps;
            rb.AddForce(this.transform.up * 500f);
            //canBackflip = true;
            doJump = false;
        }
    }

    //Reset jump counter for double jump
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            onGround = true;
            canBackflip = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            onGround = false;
            canBackflip = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Ground")
        {
            numJumps = 0;
        }
    }

    private void HandleDashReset()
    {
        if (isDashDecreasing)
        {
            float decrease = ((dashSpeed / boostDecreaseTime) * Time.fixedDeltaTime);
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
        foreach (GameObject w in wheels)
        {
            w.transform.Rotate(0, 0, -currentSpeed * wheelTurnFactor * Time.deltaTime);
        }
    }

    private void ManageSmoke()
    {
        smokeEmitter.rateOverTime = Mathf.Lerp(smokeMinRate, smokeMaxRate, (currentSpeed / maxSpeed));

    }

    private void DampenRotation()
    {
        float newAngle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, 0f, ref currentRotationVelocity, 0.1f);
        transform.rotation =
            Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                newAngle);
    }


    public void ResetPosition(Vector2 offset)
    {
        isDashDecreasing = false;
        isDashing = false;
        dashFlames.SetActive(false);


        this.transform.Translate(offset, Space.World);
        this.transform.rotation = Quaternion.identity;
        rb.freezeRotation = true;
        rb.freezeRotation = false;
        maxSpeed = DefaultMaxSpeed;
        maxBackSpeed = DefaultMaxBackSpeed;
    }

    private void Dash(bool status)
    {
        isDashing = status;
        isDashDecreasing = !status;
        if (status)
            maxSpeed += dashSpeed - dashMaxSpeedLeft;
        else
            dashMaxSpeedLeft = dashSpeed;
        dashFlames.SetActive(status);
    }

    public void AddBoost(float value)
    {
        currentBoost = Mathf.Clamp(currentBoost += value, 0, maxBoost);
    }

    public void Freeze(bool value)
    {
        if (value)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        
    }

    //Getters & setters
    public float MaxSpeed {
        get; set;
    }

    public float MaxBackSpeed {
        get; set;
    }

    public float CurrentSpeed {
        get {
            return currentSpeed;
        }
    }

    public bool CanBackflip {
        get {
            return canBackflip;
        }
        set {
            canBackflip = value;
        }
    }

    public float CurrentBoost {
        get {
            return currentBoost;
        }

        set {
            currentBoost = value;
        }
    }

    public float GetCurrentBoostPercent()
    {
        return currentBoost / maxBoost * 100f;
    }
}