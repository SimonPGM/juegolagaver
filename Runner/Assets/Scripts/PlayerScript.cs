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

    private float jumptimecounter;
    [SerializeField]
    private float jumptime;
    private bool isjumping;

    [SerializeField]
    private Animator animations; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown("space") && touchingFloor)
        {

            isjumping = true;
            jumptimecounter = jumptime;
            _playerRigidBody.AddForce(new Vector2(0, _jumpForce));
            animations.Play("JumpPJ");
        }

        if (Input.GetKey("space") && isjumping){


            if (jumptimecounter > 0){
                _playerRigidBody.velocity = Vector2.up * _jumpForce;
                jumptimecounter -= Time.deltaTime;
            }
            else {
                isjumping = false;
                animations.Play("FallPJ");

            }

        }

        if (Input.GetKeyUp("space")){
            isjumping = false;
            animations.Play("FallPJ");

        }
        
        _playerRigidBody.velocity = new Vector2(_moveSpeed, _playerRigidBody.velocity.y);

        //Debug.Log(touchingFloor);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        // Aqui iria la programación de la moneda cuando el personaje la toque.
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        animations.Play("RunPJ");
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
