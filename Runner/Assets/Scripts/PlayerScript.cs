using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject _audioSource;
    private AudioSource _audioMain;

    [SerializeField]
    private TextAsset jsonFile;
    [SerializeField]
    private TMP_InputField _namePlayer;


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
    
    [SerializeField]
    private GameObject _chontaduro;
    [SerializeField]
    private GameObject _ehda;
    [SerializeField]
    private GameObject _eche;
    [SerializeField]
    private GameObject _death;

    public Player currentPlayer = new Player();
    List<Player> players = new List<Player>();

    
    private void Awake()
    {
        currentPlayer.name = "";
        currentPlayer.score = 0;
        JObject jo = JObject.Parse(jsonFile.text);
        players = jo["Players"].ToObject<List<Player>>();
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
        score = 0;
        life = 1;
        animations.SetBool("isDead", false);
        _audioMain = _audioSource.GetComponent<AudioSource>();
        _namePlayer.gameObject.SetActive(false);
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
            currentPlayer.score += 1;
        }
        
        if (other.gameObject.CompareTag("Coin2"))
        {
            Destroy(other.gameObject);
            currentPlayer.score += 5;
        }

        if(other.gameObject.CompareTag("PowerUp1")){
            Destroy(other.gameObject);
            if (!_isInvincible){
                _isInvincible = true;
                StartCoroutine("Invulnerable");
                Instantiate(_eche);
                Instantiate(_chontaduro);
            }
        }
        
        if(other.gameObject.CompareTag("PowerUp2")){
            Destroy(other.gameObject);
            life += 1;
            Instantiate(_ehda);
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

    public void SettingPlayerName(string s)
    {
        currentPlayer.name = s;
        players.Add(currentPlayer);
        string jsonString = JsonConvert.SerializeObject(players);
        jsonString = "{ \"Players\":" + jsonString + "}";
        //Debug.Log(jsonString);
        string path = Directory.GetCurrentDirectory() + "/Assets/Scores.json";
        Debug.Log("entra if");
        File.WriteAllText(@path, jsonString);
        Destroy(this.gameObject);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    IEnumerator Invulnerable(){
        Physics2D.IgnoreLayerCollision(3,6,true);
        //Debug.Log("Invencible");
        c.a = 0.5f;
        rend.material.color = c;
        _moveSpeed = 10;
        _audioMain.mute = true;
        yield return new WaitForSeconds(10f);
        _audioMain.mute = false;
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
        _audioMain.Stop();
        Instantiate(_death);
        animations.SetBool("isDead", true);
        animations.Play("DeathPJ");
        yield return new WaitForSeconds(7f);//Delay for 5 seconds
        this.gameObject.SetActive(false);
        _namePlayer.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
