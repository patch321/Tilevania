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
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(enemySpeed, myRigidBody.velocity.y);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-enemySpeed, myRigidBody.velocity.y);
        }
        
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
