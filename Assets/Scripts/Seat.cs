using UnityEngine;

public class Seat : MonoBehaviour

{
   public bool isOccupied = false ;


   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Customer")) // replace "Player" with the tag of the trigger you want to use
        {
            isOccupied = true;
            Debug.Log("reached seat");

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
        //if (other.CompareTag("Customer")) // replace "Player" with the tag of the trigger you want to use
        //{
            //isOccupied = false;
            //Debug.Log("Customer Served");
        //}
    //}

}
