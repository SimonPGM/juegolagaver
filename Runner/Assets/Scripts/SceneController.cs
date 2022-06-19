using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
           this.transform.position = new Vector3(player.transform.position.x,
                this.transform.position.y,
                this.transform.position.z);
        }
        
    }
}