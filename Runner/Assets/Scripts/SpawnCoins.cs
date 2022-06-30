using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{   
    [SerializeField]
    public GameObject[] coinsList;
    public float respawnTime = 1f;
    private float _horizontalExtent;
    private float _maxXCamera;

    void Start()
    {
        _horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        StartCoroutine(CoinWave());
    }

    // Update is called once per frame
    void Update()
    {
        _maxXCamera = (Camera.main.transform.position.x + _horizontalExtent);
    }

    private void SpawnCoin()
    {
        respawnTime = Random.Range(1f, 3f);
        int coinIdx = Random.Range(0, coinsList.Length);
        GameObject spawned = Instantiate(coinsList[coinIdx]) as GameObject;
        float yCoordinate = GenYCoordinate(spawned);  
        spawned.transform.position = new Vector2(_maxXCamera + Random.Range(3.0f, 5.0f),yCoordinate);
    }

    private float GenYCoordinate(GameObject newCoin)
    {
        return Random.Range(-2.5f, 3f);
    }

    IEnumerator CoinWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnCoin();
        }
    }
    
}
