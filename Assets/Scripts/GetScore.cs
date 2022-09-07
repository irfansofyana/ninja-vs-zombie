using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class GetScore : MonoBehaviour
{
    private string endPoint = "http://134.209.97.218:5051/scoreboards/";
    private string nim = "13517078";
    private Text scoreboardText;

    // Start is called before the first frame update
    void Start()
    {
        scoreboardText = GetComponent<Text>();
        StartCoroutine(GetRequest(endPoint+nim));
    }

    IEnumerator GetRequest(string uri){
        UnityWebRequest request = UnityWebRequest.Get(uri);        
        yield return request.Send();
        
        string response = request.downloadHandler.text;
        var json_response = JSON.Parse(response);
        string result = "";
        for (int i = 0; i < json_response.Count; ++i){
            string username = json_response[i]["username"];
            string score = json_response[i]["score"];
            result += (i+1).ToString() + " - " + username + " - " + score + "\n";
        }
        scoreboardText.text = result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
