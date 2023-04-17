using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
   public Animator anim;
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer")) // replace "Player" with the tag of the trigger you want to use
        {
            anim.SetInteger("state", 2); // replace 1 with the state index of the animation you want to play
            Debug.Log("reached seat");

        }
    }
}
