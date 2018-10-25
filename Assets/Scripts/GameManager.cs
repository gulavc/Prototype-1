﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public Text statusText;

    public GameObject[] backgrounds;

    public Transform spawnPoint;

    private int score = 0;

    public int MaxHP = 3;
    private int hp;

	// Use this for initialization
	void Start () {
        hp = MaxHP;
        Random.InitState(SeedHolder.Seed);
        Debug.Log(SeedHolder.Seed);
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

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.ResetPosition(Vector2.zero);
    }

    public void RemoveLife(int damage = 1)
    {
        hp -= damage;
        if(hp <= 0)
        {
            //gameOver
        }
        else
        {
            RespawnPlayer();
        }
    }
    

    public int Score {
        get {
            return score;
        }
    }

    public int HP {
        get {
            return hp;
        }
    }
}
