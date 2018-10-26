using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform start, end;

    public float waitAtEnd, travelTime;

    private int currentStep = 0;
    private float timer;
    private float moveStep;
    private Vector3 target;

    private GameObject player;

	// Use this for initialization
	void Start () {
        timer = waitAtEnd;
        moveStep = (Vector2.Distance(start.position, end.position) / travelTime);
        transform.position = start.position;

        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            currentStep = (currentStep + 1) % 4;
            switch (currentStep)
            {
                case 0:
                case 2:
                    timer = waitAtEnd;
                    break;
                case 1:
                    target = end.position;
                    timer = travelTime;
                    break;
                case 3:
                    target = start.position;
                    timer = travelTime;
                    break;
                default:
                    break;
            }
        }

        if (currentStep == 1 || currentStep == 3) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveStep * Time.deltaTime);
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.transform.parent = null;
        }
    }
}
