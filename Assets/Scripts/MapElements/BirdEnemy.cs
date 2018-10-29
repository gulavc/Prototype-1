using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {


    public float flySpeed;
    public float lifeTime;
    public float minBombInterval, maxBombInterval;
    public float bombThrowForce, bombTimerMin, bombTimerMax;

    public bool flyForwards;

    public BombTrap bombPrefab;

    private float nextDrop;


	// Use this for initialization
	void Start () {
        if (flyForwards)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        nextDrop = Random.Range(minBombInterval, maxBombInterval);
	}
	
	// Update is called once per frame
	void Update () {
        nextDrop -= Time.deltaTime;
        lifeTime -= Time.deltaTime;

        if(nextDrop <= 0)
        {
            DropBomb();
            nextDrop = Random.Range(minBombInterval, maxBombInterval);
        }

        if(lifeTime <= 0)
        {
            nextDrop = float.MaxValue;
            Destroy(this.gameObject, maxBombInterval + 1);
        }

        transform.Translate(flySpeed, 0, 0);
	}

    public void DropBomb()
    {
        BombTrap drop = Instantiate(bombPrefab);
        drop.transform.parent = transform;
        drop.transform.position = transform.position;
        drop.countdownTime = Random.Range(bombTimerMin, bombTimerMax);
        drop.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * bombThrowForce);
    }
}
