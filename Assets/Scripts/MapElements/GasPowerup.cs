using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerup : MonoBehaviour {

    private GameManager gm;

    public int powerUpValue;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindObjectOfType<GameManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            gm.AddScore(powerUpValue);
            Destroy(this.gameObject);
        }
    }

}
