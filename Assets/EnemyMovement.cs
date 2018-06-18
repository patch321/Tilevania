using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float enemySpeed = 5f;

    Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        movement();
	}

    public void movement()
    {
        myRigidBody.velocity = new Vector2(enemySpeed, myRigidBody.velocity.y);
    }

    public void flipSprite()
    {

    }
}
