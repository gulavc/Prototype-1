using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionTrigger : MonoBehaviour {

    public bool hasTransitionedIntoLevel { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.transform.root.gameObject.tag == "Player")
        {
            hasTransitionedIntoLevel = true;
        }
    }

}
