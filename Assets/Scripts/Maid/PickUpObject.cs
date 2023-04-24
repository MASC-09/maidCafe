using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public float range = 5f; // The range within which the player can pick up objects
    public LayerMask layerMask; // The layer that the objects the player can pick up are on

    public GameObject Kitchen;
    private GameObject currentObject; // The object that the player is currently holding
    private Transform objectHolder;
    private Vector3 objectPosition = new Vector3(0, 1.0f, 0.4f);

    public GameObject ClientOrder;
    public GameObject Meal;

    public bool hasOrderHand = false;
    public bool hasFoodHand = false;

    void Start()
    {
        objectHolder = new GameObject("Object Holder").transform;
        objectHolder.SetParent(transform);
    }

    void Update()
{
    if (currentObject == null) // If the player isn't holding an object
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the mouse position

        if (Physics.Raycast(ray, out hit, range, layerMask)) // If the ray hits an object on the specified layer within the range
        {

            if (hit.collider.CompareTag("Food") || hit.collider.CompareTag("Order")) // If the object is tagged as pickupable
            {
                string collisionTag = hit.collider.tag;
                Debug.Log("player Picked up " + collisionTag);
                if (Input.GetKeyDown(KeyCode.E)) // If the player presses the E key
                {
                    currentObject = hit.collider.gameObject; // Set the current object to the hit object
                    currentObject.transform.SetParent(transform); // Set the hit object's parent to the player
                    currentObject.transform.localPosition = objectPosition;  // Move the object slightly in front of the player
                    currentObject.GetComponent<Rigidbody>().isKinematic = true; // Set the object's rigidbody to kinematic
                    
                    if (collisionTag.Equals("Food"))
                    {
                        hasFoodHand = true;
                        hasOrderHand = false;
                    }
                    else if (collisionTag.Equals("Order"))
                    {
                        hasOrderHand = true;
                        hasFoodHand = false;
                    }
                }
            }
            else if (hit.collider.CompareTag("Customer"))
            {
                if (Input.GetKeyDown(KeyCode.E)) // If the player presses the E key
                {
                    // if(currentObject.tag == "Food")
                    // {
                    //     Debug.Log("Colliding with customer");
                    //     Destroy(currentObject); // Destroy the current object
                    //     currentObject = null; // Set the current object to null
                    //     hasFoodHand = false;
                    //     // Customer.ClientServed(); 
                    // }
                    // else
                    // {
                        NavMeshController customer = hit.collider.GetComponent<NavMeshController>();
                        currentObject = Instantiate(ClientOrder, transform.position + transform.forward * 2f, Quaternion.identity);
                        currentObject.transform.SetParent(transform); // Set the hit object's parent to the player
                        currentObject.transform.localPosition = objectPosition;  // Move the object slightly in front of the player
                        currentObject.GetComponent<Rigidbody>().isKinematic = true; // Set the object's rigidbody to kinematic
                        customer.setHasOrdered(true);
                        Debug.Log("Customer has Ordered.");
                        hasOrderHand = true;
                    // }
                }
            }
        }
    }
    else // If the player is holding an object
    {
        if (Input.GetKeyDown(KeyCode.E)) // If the player presses the E key
        {
            currentObject.transform.SetParent(null); // Set the object's parent to null
            currentObject.GetComponent<Rigidbody>().isKinematic = false; // Set the object's rigidbody to non-kinematic
            currentObject = null; // Set the current object to null
            hasOrderHand = false;
            hasFoodHand = false;
        }
    }
}


    
   private void OnTriggerEnter(Collider other) 
   {
    //player deliver order in kitchen
    if (other.CompareTag("Deliver") && currentObject != null )
        {
            if(hasOrderHand)
            {
                Kitchen kitchenInstance = FindObjectOfType<Kitchen>();
                Destroy(currentObject); // Destroy the current object
                currentObject = null; // Set the current object to null
                hasOrderHand = false;
                kitchenInstance.StartCooking();
            }
            else if ( hasFoodHand)
            {
                Destroy(currentObject); // Destroy the current object
                currentObject = null; // Set the current object to null
                hasFoodHand = false;
            }
            
        }
        //player deliver food to customer
    // else if ( other.CompareTag("Customer") && currentObject!= null && hasFoodHand == true)
    //     {
    //         // Debug.Log("Colliding with customer");
    //         // Destroy(currentObject); // Destroy the current object
    //         // currentObject = null; // Set the current object to null
    //         // hasFoodHand = false;
    //         // // Customer.ClientServed(); 
    //     }
    }

    
}
