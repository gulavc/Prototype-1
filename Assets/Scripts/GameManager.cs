using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PlayerController player;
    public Text statusText;

    public Transform spawnPoint;    

    public int MaxHP = 3;

    public int Score { get; private set; } = 0;
    public int HP { get; private set; }

    public LevelGenerator lg;
    public GameOverUI gameOverUI;
    public UIManager gameUI;

    public int levelLength;

    // Use this for initialization
    void Start () {       
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
        player.Freeze(false);
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
                GameOver(false);
            }
            else
            {
                RespawnPlayer();
            }
        }

    }


    private void StartGame()
    {
        Random.InitState(SeedHolder.Seed);
        lg.GenerateInitialMap(levelLength);
        RespawnPlayer();
                
        gameUI.Hide(false);
        gameOverUI.Hide(true);

        HP = MaxHP;
        player.CurrentBoost = player.maxBoost / 2f;
    }

    public void GameOver(bool winGame)
    {
        player.Freeze(true);
        gameUI.Hide(true);
        gameOverUI.GameOver(winGame);
    }

    public void RestartGame()
    {
        Score = 0;
        lg.ClearLevel();
        StartGame();
    }



}
