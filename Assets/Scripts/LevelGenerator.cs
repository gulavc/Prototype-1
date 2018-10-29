﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    
    public Level[] levelPool;

    private LinkedList<Level> currentLevel;
    private GameManager gm;

    private int fixedOrder;

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
        else
        {
            return levelPool[Random.Range(0, levelPool.Length)];
        }

    }


    public void GenerateInitialMap()
    {
        for(int i = 0; i < 5; ++i)
        {            
            Level l = Instantiate(GetRandomLevel());
            if(currentLevel.Count > 0)
            {                
                l.transform.position = currentLevel.Last.Value.endPoint.position;
            }
            else
            {
                l.transform.position = Vector3.zero;
                gm.spawnPoint = l.spawnPoint;
            }
            
            currentLevel.AddLast(l);

        }
    }

    public void ClearLevel()
    {
        foreach (Level l in currentLevel)
        {
            Destroy(l);            
        }
        currentLevel.Clear();
    }

}
