using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightTrap : Trap
{
    public float fallSpeed = 1f;

    private Rigidbody2D rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsTrapHit)
        {
            IsTrapDamageDone = true;
            gameManager.RemoveLife();
            IsTrapHit = false;
        }
    }

    public override void FireTrap()
    {
        IsTrapTriggered = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!IsTrapDamageDone && collision.gameObject.tag == "Player")
        {
            IsTrapHit = true;
        }
        if(!IsTrapDamageDone && collision.gameObject.tag == "Ground")
        {
            IsTrapDamageDone = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            IsTrapDamageDone = false;
        }
    }

}