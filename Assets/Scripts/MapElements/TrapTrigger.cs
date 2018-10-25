using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour {

    public Trap t;	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!t.IsTrapTriggered && collision.transform.root.gameObject.tag == "Player")
        {
            t.FireTrap();
        }
    }
}
