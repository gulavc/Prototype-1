using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestroy : MonoBehaviour {

    public BombTrap bombTrap;

    public void Destroy()
    {
        bombTrap.DeactivateTrap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.root.gameObject.tag == "Player")
        {
            bombTrap.HitPlayer();
        }           

    }
}
