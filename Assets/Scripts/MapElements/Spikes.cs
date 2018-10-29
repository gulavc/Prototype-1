using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    private GameManager gameManager;
    private bool spikesHit;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        spikesHit = false;
	}

    private void Update()
    {
        if (spikesHit)
        {
            spikesHit = false;
            gameManager.RemoveLife();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spikesHit = true;
        }
    }

}
