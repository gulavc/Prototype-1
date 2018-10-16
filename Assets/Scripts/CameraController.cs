using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public PlayerController target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(target.transform.position.x - this.transform.position.x, 0, 0);
        //this.transform.RotateAround(target.transform.position, Vector3.up, 0.1f * target.CurrentSpeed);
	}
}
