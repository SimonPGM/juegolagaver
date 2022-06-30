using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{   
    [SerializeField]
    public GameObject[] powerUpsList;
    public float respawnTime = 1f;
    private float _horizontalExtent;
    private float _maxXCamera;

    void Start()
    {
        _horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        StartCoroutine(powerUpWave());
    }

    // Update is called once per frame
    void Update()
    {
        _maxXCamera = (Camera.main.transform.position.x + _horizontalExtent);
    }

    private void SpawnpowerUp()
    {
        respawnTime = Random.Range(1f, 3f);
        int powerUpIdx = Random.Range(0, powerUpsList.Length);
        GameObject spawned = Instantiate(powerUpsList[powerUpIdx]) as GameObject;
        float yCoordinate = GenYCoordinate(spawned);  
        spawned.transform.position = new Vector2(_maxXCamera + Random.Range(3.0f, 5.0f),yCoordinate);
    }

    private float GenYCoordinate(GameObject newpowerUp)
    {
        return Random.Range(-2.5f, 3f);
    }

    IEnumerator powerUpWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnpowerUp();
        }
    }
    
}