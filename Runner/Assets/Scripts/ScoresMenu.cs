using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoresMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private TMP_Text[] names;
    
    [SerializeField]
    private TMP_Text[] scores;
    
    //[SerializeField]
    private StreamReader jsonFile;
    
    
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
    
    List<Player> players = new List<Player>();
    private JObject jo;
    private void Awake()
    {
        jsonFile = new StreamReader(Application.persistentDataPath + "/Scores.json");
        JObject jo = JObject.Parse(jsonFile.ReadToEnd());
        players = jo["Players"].ToObject<List<Player>>();
        jsonFile.Close();
        players.Sort();
    }

    void Start()
    {
        int cnt = 0;
        int n = players.Count - 1;
        if (n >= 0)
        {
            int lower = math.max(0, n - 2);
            for (int i = n; i >= lower; i--)
            {
                names[cnt].text = players[i].name;
                scores[cnt].text = "" + players[i].score;
                cnt += 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateJsonRead();
        int cnt = 0;
        int n = players.Count - 1;
        if (n >= 0)
        {
            int lower = math.max(0, n - 2);
            for (int i = n; i >= lower; i--)
            {
                names[cnt].text = players[i].name;
                scores[cnt].text = "" + players[i].score;
                cnt += 1;
            }
        }
    }

    public void updateJsonRead()
    {
        jsonFile = new StreamReader(Application.persistentDataPath + "/Scores.json");
        JObject jo = JObject.Parse(jsonFile.ReadToEnd());
        players = jo["Players"].ToObject<List<Player>>();
        jsonFile.Close();
        players.Sort();
    }
}
