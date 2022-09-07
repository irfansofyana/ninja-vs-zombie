using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour
{
    private string endPoint = "http://134.209.97.218:5051/scoreboards/";
    private string nim = "13517078";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        Zombie.scoreNinja = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void GetInputUsername(string username){
        Debug.Log("You entered Username: " + username);
        postScore(username, Zombie.scoreNinja);
    }

    public void Leaderboard(){
        SceneManager.LoadScene("LeaderBoardScene");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenuScene");
    }

    public void postScore(string username, int score){
        StartCoroutine(insertScore(username, score));
    }

    IEnumerator insertScore(string username, int score){
        var request = new UnityWebRequest(endPoint+nim, "POST");
        string scoreString = score.ToString();
        string user = "username";
        string scoreUser = "score";
        string bodyJsonString = "{" + 
                                    '"' + user + '"'  +  ": " + '"' + username +  '"' + ","+ 
                                    '"' + scoreUser + '"' + ": " + scoreString + 
                                "}";
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();

        Debug.Log("Response: " + request.downloadHandler.text);
    }
}
