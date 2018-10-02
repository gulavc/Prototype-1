using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerController player;

    public GameObject[] backgrounds;

	// Use this for initialization
	void Start () {
		foreach(GameObject b in backgrounds)
        {
            b.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
