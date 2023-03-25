using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;
    public Vector3 playerInput;
    private Vector3 camFoward; //donde esta viendo la camera
    private Vector3 camRight;
    public Camera mainCamera;
    private Vector3 movePlayer; //to turn the player. 
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpVelocity;

    public Animator playerAnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("WalkVelocity", playerInput.magnitude * playerSpeed);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camFoward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        player.Move(movePlayer * playerSpeed * Time.deltaTime);
    }

    //Get's the camera's direction
    //get the cameras direction and saves it  the global variblas camForward and camRight
    private void camDirection()
    {
        camFoward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camFoward.y = 0;
        camRight.y = 0;

        camFoward = camFoward.normalized;
        camRight = camRight.normalized;

    }

    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetFloat("VerticalVelocity", player.velocity.y);
        }
        playerAnimatorController.SetBool("IsGrounded", player.isGrounded);

        //SlideDown();
    }

    void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButton("Jump"))
        {
            fallVelocity = jumpVelocity;
            movePlayer.y = fallVelocity;
            playerAnimatorController.SetTrigger("Jump");
        }

    }

    private void OnAnimatorMove()
    {

    }
}
