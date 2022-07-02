using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int score;
    private int life;
    
    /*[SerializeField]
    private TextAsset jsonFile;
    
    
    public class  Player: IComparable
    {
        public string name;
        public int score;

        public int CompareTo(object obj)
        {
            Player otherPlayer = obj as Player;
            return this.score.CompareTo(otherPlayer.score);
        }
    }

    public Player currentPlayer = new Player();
    public Player[] players;
    

    private void Awake()
    {
        currentPlayer.name = "sizas";
        currentPlayer.score = 0;
    }*/

    [SerializeField]
    private GameObject _chontaduro;
    [SerializeField]
    private GameObject _ehda;
    [SerializeField]
    private GameObject _eche;
    [SerializeField]
    private GameObject _death;

    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
        score = 0;
        life = 1;
        animations.SetBool("isDead", false);
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
        

        //Debug.Log("The class one zuckas: " + currentPlayer.score);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag("Coin1"))
        {
            Destroy(other.gameObject);
            score += 1;
        }
        
        if (other.gameObject.CompareTag("Coin2"))
        {
            Destroy(other.gameObject);
            score += 5;
        }

        if(other.gameObject.CompareTag("PowerUp1")){
            Destroy(other.gameObject);
            if (!_isInvincible){
                Instantiate(_eche);
                Instantiate(_chontaduro);
                _isInvincible = true;
                StartCoroutine("Invulnerable");
            }
        }
        
        if(other.gameObject.CompareTag("PowerUp2")){
            Destroy(other.gameObject);
            life += 1;
        }
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (life > 0) animations.Play("RunPJ");
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 3)
        {
            life -= 1;
            Destroy(col.gameObject);
            if (life == 0)
            {
                _moveSpeed = 0f;
                StartCoroutine("DelayedDeath");
            }
            //Debug.Log(JsonUtility.ToJson(currentPlayer));
        }
    }

    IEnumerator Invulnerable(){
        Physics2D.IgnoreLayerCollision(3,6,true);
        //Debug.Log("Invencible");
        c.a = 0.5f;
        rend.material.color = c;
        _moveSpeed = 10;
        yield return new WaitForSeconds(10f);
        c.a = 1f;
        rend.material.color = c;
        _moveSpeed = 6;
        Physics2D.IgnoreLayerCollision(3,6,false);
        //Debug.Log("No Invencible");
        _isInvincible = false;
    }
    
    IEnumerator DelayedDeath()
    {
        //Debug.Log("Hero is dying");//Launch the animation and stuffs
        animations.SetBool("isDead", true);
        animations.Play("DeathPJ");
        yield return new WaitForSeconds(2f);//Delay for 5 seconds
        Destroy(this.gameObject);
        SceneManager.LoadScene("Menu");
        //Debug.Log("Hero is dead");//ProcessPlayerDeath
 
    }
    
}
