using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{   
    [SerializeField]
    public GameObject[] powerUpsList;
    public float respawnTime = 3.0f;
    private float _horizontalExtent;
    private float _maxXCamera;
    private float[] _wts = new float[2];

    void Start()
    {
        _horizontalExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
        StartCoroutine(powerUpWave());
        _wts[0] = 0.4f;
        _wts[1] = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        _maxXCamera = (Camera.main.transform.position.x + _horizontalExtent);
    }

    private void SpawnpowerUp()
    {
        respawnTime = Random.Range(15.0f, 30.0f);
        int powerUpIdx = GetRandomWeightedIndex(_wts);
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
    
    public int GetRandomWeightedIndex(float[] weights)
    {
        // Get the total sum of all the weights.
        float weightSum = 0f;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }
 
        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = powerUpsList.Length - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }
 
            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }
 
        // No other item was selected, so return very last index.
        return index;
    }
    
}