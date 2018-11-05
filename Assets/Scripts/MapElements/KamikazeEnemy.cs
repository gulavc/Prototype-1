using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour {


    public float flySpeed;
    public float aggroRadius;
    private PlayerController target;
    private Transform startPos;
    private bool hitPlayer;
    private GameManager gm;

    private SpriteRenderer[] sr;

	// Use this for initialization
	void Start () {

        target = GameObject.FindObjectOfType<PlayerController>();
        gm = GameObject.FindObjectOfType<GameManager>();
        startPos = transform;
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(target.transform.position, transform.position) <= aggroRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, flySpeed * Time.deltaTime);
        }
        else if(Vector3.Distance(target.transform.position, transform.position) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, flySpeed * Time.deltaTime);
        }

        

        if (hitPlayer)
        {
            gm.RemoveLife();
            hitPlayer = false;
            Destroy(this.gameObject);
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            hitPlayer = true;
        }
    }

}
