using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public Text statusText;

    public GameObject[] backgrounds;

    private int score = 0;

	// Use this for initialization
	void Start () {
		foreach(GameObject b in backgrounds)
        {
            //b.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void BackflipDone()
    {
        player.CanBackflip = true;
        AddScore(40);
    }

    public void AddScore(int value)
    {
        score += value;
        //start coroutine: score popup!
        player.AddBoost(value/100f);
    }

    public int Score {
        get {
            return score;
        }
    }
}
