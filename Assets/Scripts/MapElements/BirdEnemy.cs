using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {


    public float flySpeed;
    public float pathDuration;
    public float minBombInterval, maxBombInterval;
    public float bombThrowForce, bombTimerMin, bombTimerMax;

    public bool flyForwards;

    public BombTrap bombPrefab;

    private float nextDrop;
    private float pathTime;

    private SpriteRenderer[] sr;

	// Use this for initialization
	void Start () {

        sr = GetComponentsInChildren<SpriteRenderer>();

        if (flyForwards)
        {
            foreach (SpriteRenderer sprite in sr)
            {
                sprite.flipX = !sprite.flipX;
            }
        }

        nextDrop = Random.Range(minBombInterval, maxBombInterval);
        pathTime = pathDuration;
        flySpeed = flyForwards ? flySpeed : -flySpeed;
    }
	
	// Update is called once per frame
	void Update () {
        nextDrop -= Time.deltaTime;
        pathTime -= Time.deltaTime;

        if(nextDrop <= 0)
        {
            DropBomb();
            nextDrop = Random.Range(minBombInterval, maxBombInterval);
        }

        if(pathTime <= 0)
        {
            pathTime = pathDuration;
            foreach (SpriteRenderer sprite in sr)
            {
                sprite.flipX = !sprite.flipX;
            }
            flySpeed = -flySpeed;
        }        

        transform.Translate(flySpeed, 0, 0);
	}

    public void DropBomb()
    {
        BombTrap drop = Instantiate(bombPrefab);
        drop.transform.parent = null;
        drop.transform.position = transform.position;
        drop.countdownTime = Random.Range(bombTimerMin, bombTimerMax);
        drop.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * bombThrowForce);
    }
}
