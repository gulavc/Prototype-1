using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public InputField seedField;

    private int seed;

    public GameObject ControlsUIPanel;

	// Use this for initialization
	void Start () {
        seed = (int)System.DateTime.Now.Ticks;
        seedField.text = seed.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        Int32.TryParse(seedField.text, out seed);
        SeedHolder.Seed = seed;

        SceneManager.LoadScene("Platformer");
    }

    public void ShowHideControls()
    {
        ControlsUIPanel.SetActive(!ControlsUIPanel.activeSelf);
    }
}
