using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemiesList;
    public float respawnTime = 2f;
    private float _horizontalExtent;
    private float _maxXCamera;
    void Start()
    {
        _horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        StartCoroutine(EnemiesWave());
    }

    // Update is called once per frame
    void Update()
    {
        _maxXCamera = (Camera.main.transform.position.x + _horizontalExtent);
    }

    private void SpawnEnemy()
    {
        respawnTime = Random.Range(1.5f, 2.0f);
        int enemyIdx = Random.Range(0, enemiesList.Length);
        GameObject spawned = Instantiate(enemiesList[enemyIdx]) as GameObject;
        float yCoordinate = GenYCoordinate(spawned);  
        spawned.transform.position = new Vector2(_maxXCamera + Random.Range(3.0f, 5.0f),yCoordinate);
    }

    private float GenYCoordinate(GameObject newEnemy)
    {
        if (newEnemy.CompareTag("Static"))
        {
            return -4.65f;
        }
        else if (newEnemy.CompareTag("Terrestrial"))
        {
            return -3.72f;
        }

        return Random.Range(-2.5f, -0.5f);
    }

    IEnumerator EnemiesWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
    
}
