using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public GameObject[] _BlockPreFab;
    public float _playerPointer;
    public float _safePlaceGenerator = 12;


    // Start is called before the first frame update
    void Start()
    {
        _playerPointer = -7;
    }

    private Coroutine _test;
    void Update()
    {
        if (player != null)
        {
            this.transform.position = new Vector3(player.transform.position.x,
                 this.transform.position.y,
                 this.transform.position.z);
        }
        Debug.Log("While");

        if (_test != null)
        {
            StopCoroutine(_test);
        }
        _test = StartCoroutine(EndlessRunner());

    }

    IEnumerator EndlessRunner()
    {
        while (player != null && _playerPointer < player.transform.position.x + _safePlaceGenerator)
        {
            int indexBlock = Random.Range(0, _BlockPreFab.Length - 1);
            if (_playerPointer < 0)
            {
                indexBlock = 0;
            }
            GameObject ObjectBlock = Instantiate(_BlockPreFab[indexBlock]);
            ObjectBlock.transform.SetParent(this.transform);
            Bloque block = ObjectBlock.GetComponent<Bloque>();
            ObjectBlock.transform.position = new Vector2(_playerPointer + block._size / 2, 0);

            yield return null;
        }
    }
}