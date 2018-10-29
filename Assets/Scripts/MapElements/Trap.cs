using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour {

    public abstract void FireTrap();

    public GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public bool IsTrapTriggered { get; set; } = false;

    public bool IsTrapDamageDone { get; set; } = false;

    public bool IsTrapHit { get; set; } = false;
}
