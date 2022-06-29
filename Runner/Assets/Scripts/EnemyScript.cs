using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _rb;
    
    private float _horizontalExtent;
    private float _minxCamera;

    void Start()
    {
        _speed = this.CompareTag("Static") ? 0 : 2;
        _rb = this.GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(-_speed, 0);
        _horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        _minxCamera = (Camera.main.transform.position.x - _horizontalExtent);
        if (transform.position.x < _minxCamera)
        {
            Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Floor"))
        {
            Destroy(col.gameObject);
            if(this.gameObject != null) Destroy(this.gameObject);
        }
        
    }*/
}
