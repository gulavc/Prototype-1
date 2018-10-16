using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public PlayerController player;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		if(player.CurrentSpeed > 10)
        {
            animator.SetBool("Forward", true);
        }
        else
        {
            animator.SetBool("Forward", false);
        }
	}

    public void Backflip()
    {
        animator.SetTrigger("Backflip");
    }

    public void BackflipDone()
    {
        player.CanBackflip = true;
    }
}
