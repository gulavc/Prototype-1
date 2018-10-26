using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour {

    public GameManager gameManager;
    public PlayerController player;

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + new Vector3(0, -50, 0);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(player.transform.position.x - transform.position.x, 0, 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.gameObject.tag == "Player")
        {
            gameManager.RemoveLife();
        }
    }
}
