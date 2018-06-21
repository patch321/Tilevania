using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

	// Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;

	// State
	bool isAlive = true;

	// Cached component References
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float regGravity;
    float animatorSpeed;

	// Messages then Methods
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        regGravity = myRigidBody.gravityScale;
        animatorSpeed = myAnimator.speed;
	}

	void Update()
	{
		Run();
		Jump();
		ClimbLadder();
		FlipSprite();
	}

	private void Run()
	{
		float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value is between -1 and 1
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;

		bool isPlayerMoving = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

		if (isPlayerMoving)
		{
			myAnimator.SetBool("Running", true);
		}
		else
		{
			myAnimator.SetBool("Running", false);
		}
	}

	private void Jump()
	{
		if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
			{
				Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
				myRigidBody.velocity += jumpVelocityToAdd;
			}
		}
	}

	private void ClimbLadder()
	{
		if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) // if feet are not touching ladder
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = regGravity;
            myAnimator.speed = animatorSpeed;
            return;
        }

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = regGravity;
            myAnimator.speed = animatorSpeed;
        }
        else
        {
            myAnimator.SetBool("Climbing", true);
            myRigidBody.gravityScale = Mathf.Epsilon;
        }

		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value is between -1 and 1
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);

        if (controlThrow == 0 && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.speed = 0f;
        }
        else
        {
            myAnimator.speed = animatorSpeed;
        }
	}

	private void FlipSprite()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

		if (playerHasHorizontalSpeed)
		{
			myRigidBody.transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
		}
	}
}
