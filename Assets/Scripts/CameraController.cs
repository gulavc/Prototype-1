using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float horizontalOffset = 25f;
    public float verticalOffset = 16f;
    public float depthOffset = -50f;
    public float smoothTime = 0.3f;

    public float maxFollowDistance = 50f;

    private Vector3 velocity = Vector2.zero;
    private Vector3 targetOffset;

    // Use this for initialization
    void Start () {
        targetOffset = new Vector3(horizontalOffset, verticalOffset, depthOffset);
        transform.position = target.position + targetOffset;
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + targetOffset, ref velocity, smoothTime);

	}
}
