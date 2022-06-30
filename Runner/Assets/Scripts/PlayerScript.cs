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

    private bool _isInvincible = false;
    private Renderer rend;
    private Color c;

    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
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

        if (other.gameObject.CompareTag("Coin1"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("PowerUp1")){
            Destroy(other.gameObject);
            if (!_isInvincible){
                _isInvincible = true;
                StartCoroutine("Invulnerable");
            }

        }

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

    

    IEnumerator Invulnerable(){
        Physics2D.IgnoreLayerCollision(3,6,true);
        Debug.Log("Invencible");
        c.a = 0.5f;
        rend.material.color = c;
        _moveSpeed = 10;
        yield return new WaitForSeconds(10f);
        c.a = 1f;
        rend.material.color = c;
        _moveSpeed = 6;
        Physics2D.IgnoreLayerCollision(3,6,false);
        Debug.Log("No Invencible");
        _isInvincible = false;  

    }
}
