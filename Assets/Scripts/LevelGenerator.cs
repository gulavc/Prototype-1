﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    
    public Level[] levelPool;

    public Level victoryLevel;

    private LinkedList<Level> currentLevel;
    private GameManager gm;

    private int fixedOrder;

    private GameObject LevelHolder;

    // Use this for initialization

    void Start () {
        currentLevel = new LinkedList<Level>();
        gm = GameObject.FindObjectOfType<GameManager>();
        fixedOrder = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(currentLevel.Count > 0)
        {
            foreach (Level l in currentLevel)
            {
                if (l.lt.hasTransitionedIntoLevel)
                {
                    l.lt.hasTransitionedIntoLevel = false;
                    gm.spawnPoint = l.spawnPoint;

                    if (l.lt.victory == true)
                    {
                        gm.GameOver(true);
                    }
                }
            }
        }
		
	}

    public Level GetRandomLevel()
    {
        if (SeedHolder.Seed == 0)
        {
            int value = fixedOrder % levelPool.Length;
            fixedOrder++;
            return levelPool[value];            
        }
        else if(SeedHolder.Seed <= levelPool.Length && SeedHolder.Seed > 0)
        {
            return levelPool[SeedHolder.Seed - 1];
        }
        else
        {
            return levelPool[Random.Range(0, levelPool.Length)];
        }

    }


    public void GenerateInitialMap(int levelLength = 5)
    {

        LevelHolder = new GameObject("level holder");
        Level l;

        for (int i = 0; i < levelLength; ++i)
        {            
            l = Instantiate(GetRandomLevel());
            if(currentLevel.Count > 0)
            {                
                l.transform.position = currentLevel.Last.Value.endPoint.position;
            }
            else
            {
                l.transform.position = Vector3.zero;                
            }

            l.transform.parent = LevelHolder.transform;
            currentLevel.AddLast(l);

        }

        l = Instantiate(victoryLevel);
        if (currentLevel.Count > 0)
        {
            l.transform.position = currentLevel.Last.Value.endPoint.position;
        }
        else
        {
            l.transform.position = Vector3.zero;
        }
        l.transform.parent = LevelHolder.transform;
        currentLevel.AddLast(l);

        gm.spawnPoint = currentLevel.First.Value.spawnPoint;
    }

    public void ClearLevel()
    {
        Destroy(LevelHolder);
        currentLevel.Clear();
        fixedOrder = 0;
    }

}
