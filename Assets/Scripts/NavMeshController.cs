using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public float movementSpeed;
    private enum animationState {idle, walking, sit_idle, sit_pointing, sit_angry};
    public Animator anim;
    private bool isAtSeat;
    private bool isMoving; //this could be replaced for just a value greater than 0 on the movement vector
    public bool hasOrdered = false;
    public bool fewTime;



    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.destination = target.position;
        agent.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        isMoving = agent.velocity.magnitude > 0.1f; //Check if agent is moving
        UpdateAnimationState();
        // Check_ReachSeat();


    }

    private void UpdateAnimationState()
    {
        animationState state;
        state = animationState.idle;

        if (isMoving && !isAtSeat)
        {
            state = animationState.walking;
        }
        if (isAtSeat)
        {
            state = animationState.sit_idle;
            // if (!hasOrdered)    
            // {
            //     state = animationState.sit_pointing;
            // }
            // else if (fewTime)
            // {
            //     state = animationState.sit_idle;
            // }
            // else 
            // {
            //     state = animationState.sit_angry;
            // }
        }
        anim.SetInteger("state",(int)state);
        // Debug.Log("current state " + state);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("seat"))
        {
            isAtSeat = true;
            Debug.Log("reached seat");
            anim.SetInteger("state",(int)animationState.sit_idle);        
        }
    }

    
}
