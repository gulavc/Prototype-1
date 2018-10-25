using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    
    public GameManager gm;
    public Text speedText;
    public Text scoreText;
    public ProgressBar pb;
    public Text hpText;
    
    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = gm.player;
	}
	
	// Update is called once per frame
	void Update () {
        pb.BarValue = Mathf.Round(player.GetCurrentBoostPercent());
        speedText.text = "Current Speed: " + Mathf.RoundToInt(player.CurrentSpeed).ToString();
        scoreText.text = "Score: " + gm.Score;
        hpText.text = "HP: " + gm.HP;
    }
}
