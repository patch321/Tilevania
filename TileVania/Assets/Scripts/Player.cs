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
	Collider2D myCollider;

	// Messages then Methods
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		myCollider = GetComponent<Collider2D>();
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
		if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

		if (CrossPlatformInputManager.GetButtonDown("Jump"))
		{
			if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
			{
				Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
				myRigidBody.velocity += jumpVelocityToAdd;
				print("Jumping");
			}
		}
	}

	private void ClimbLadder()
	{
		if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { return; }

		float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value is between -1 and 1
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
		myRigidBody.velocity = climbVelocity;

		bool isPlayerClimbing = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

		if (isPlayerClimbing)
		{
			myAnimator.SetBool("Climbing", true);
		}
		else
		{
			myAnimator.SetBool("Climbing", false);
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
