using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour {

    private bool isTrapTriggered = false;
    private bool isTrapDamageDone = false;

    public abstract void FireTrap();

    public GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public bool IsTrapTriggered {
        get {
            return isTrapTriggered;
        }

        set {
            isTrapTriggered = value;
        }
    }

    public bool IsTrapDamageDone {
        get {
            return isTrapDamageDone;
        }

        set {
            isTrapDamageDone = value;
        }
    }
}
