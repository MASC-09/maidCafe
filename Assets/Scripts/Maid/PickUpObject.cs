using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public float range = 5f; // The range within which the player can pick up objects
    public LayerMask layerMask; // The layer that the objects the player can pick up are on

    private GameObject currentObject; // The object that the player is currently holding
    private Transform objectHolder;
    private Vector3 objectPosition = new Vector3(0, 1.0f, 0.4f);

    public GameObject ClientOrder;
    public GameObject Meal;

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
                if (hit.collider.CompareTag("Pickupable")) // If the object is tagged as pickupable
                {
                    if (Input.GetKeyDown(KeyCode.E)) // If the player presses the E key
                    {
                        currentObject = hit.collider.gameObject; // Set the current object to the hit object
                        currentObject.transform.SetParent(transform); // Set the hit object's parent to the player
                        currentObject.transform.localPosition = objectPosition;  // Move the object slightly in front of the player
                        currentObject.GetComponent<Rigidbody>().isKinematic = true; // Set the object's rigidbody to kinematic
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
            }
        }
    }

    //Method for when the player enters the intraction radius of a customer.
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Interaction") && currentObject == null)
        {
            Debug.Log("Entered Ineraction Area.");

            NavMeshController customer = other.GetComponentInParent<NavMeshController>();
            if(!customer.hasOrdered)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    // Instantiate the client order prefab and set it as the current object
                    currentObject = Instantiate(ClientOrder, transform.position + transform.forward * 2f, Quaternion.identity);
                    currentObject.transform.SetParent(transform); // Set the order's parent to the player
                    currentObject.GetComponent<Rigidbody>().isKinematic = true; // Set the order's rigidbody to kinematic
                    customer.setHasOrdered(true);
                    Debug.Log("Customer has Ordered.");
                }
            }
        }
        else if(other.CompareTag("Deliver") && currentObject != null)
        {   
            if(Input.GetKeyDown(KeyCode.E))
            {
                Destroy(currentObject);
                //Oder Delivered()
            }
        }   
    }

    
}
