using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public Text statusText;

    public Transform spawnPoint;
    public int MaxHP = 3;
    private ArrayList traps;

    public int Score { get; private set; } = 0;
    public int HP { get; private set; }

    public LevelGenerator lg;

    // Use this for initialization
    void Start () {
        HP = MaxHP;
        Random.InitState(SeedHolder.Seed);       

        
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void BackflipDone()
    {
        player.CanBackflip = true;
        AddScore(20);
    }

    public void AddScore(int value)
    {
        Score += value;
        //start coroutine: score popup!
        player.AddBoost(value/100f);
    }

    public void RespawnPlayer()
    {
        player.transform.position = spawnPoint.position;
        player.ResetPosition(Vector2.zero);
        player.IsImmuneToDamage = false;
    }

    public void RemoveLife(int damage = 1)
    {
        if (!player.IsImmuneToDamage)
        {
            HP -= damage;
            player.IsImmuneToDamage = true;
            if (HP <= 0)
            {
                //gameOver
                Debug.Log("Game over");
            }
            else
            {
                RespawnPlayer();
            }
        }

    }


    private void StartGame()
    {
        lg.GenerateInitialMap();
        RespawnPlayer();
    }
    
    

}
