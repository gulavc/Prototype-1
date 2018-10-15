using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public Text statusText;

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

        statusText.text = "Current Speed: " + player.CurrentSpeed.ToString();

	}
}
