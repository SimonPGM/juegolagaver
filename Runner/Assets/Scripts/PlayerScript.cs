using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _moveSpeed;

    private bool touchingFloor = false;
    private Rigidbody2D _playerRigidBody; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown("space") && touchingFloor)
        {
            _playerRigidBody.AddForce(new Vector2(0, _jumpForce));
        }
        
        _playerRigidBody.velocity = new Vector2(_moveSpeed, _playerRigidBody.velocity.y);

        Debug.Log(touchingFloor);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        touchingFloor = true;
        /*if (col.CompareTag("Obstacle"))
        {
            GameObject.Destroy(this.gameObject);
        }*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touchingFloor = false;
    }
}
