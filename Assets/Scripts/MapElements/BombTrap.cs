using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombTrap : Trap {

    public float countdownTime = 5f;
    public GameObject bombHitbox;

    public Text countdownText;
    

	void Awake () {        
        bombHitbox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        countdownTime -= Time.deltaTime;
        if(countdownTime <= 0 && !IsTrapTriggered)
        {
            FireTrap();
            countdownText.enabled = false;
        }
        else
        {
            countdownText.text = Mathf.FloorToInt(countdownTime).ToString();
        }

        if (IsTrapHit)
        {
            IsTrapDamageDone = true;
            gameManager.RemoveLife();
            IsTrapHit = false;
        }
	}

    public override void FireTrap()
    {
        IsTrapTriggered = true;
        bombHitbox.SetActive(true);
    }

    public void DeactivateTrap()
    {
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public void HitPlayer()
    {
        if (!IsTrapDamageDone)
        {
            IsTrapHit = true;
        }

    }
    
}
