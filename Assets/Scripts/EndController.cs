using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using HighScores;
using UnityEngine.Networking;

public class EndController : MonoBehaviour
{
    public TMP_Text ScoreField;
    public TMP_Text TimeField;
    public TMP_InputField NameField;
    private float score;
    const string urlBackendHighScores = "https://example.com/database/gameWithPHP/api/highscores.php";

    private void Start()
    {
        GameObject ScoreButton = GameObject.FindGameObjectWithTag("ScoreButton");
        ScoreButton.SetActive(true);
        if (Assets.Globals.currentTime > 360)
        {
            score = 0;
        }   else
        {
            score = 360 - Assets.Globals.currentTime;
        }
        ScoreField.text = score.ToString();
        TimeField.text = Assets.Globals.currentTime.ToString() + " s";
    }

    void Update()
    {

    }

    public void LoadMain()
    {
        SceneManager.LoadScene("main");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void SendScores()
    {
        GameObject ScoreButton = GameObject.FindGameObjectWithTag("ScoreButton");
        HighScores.HighScore hsItem = new HighScores.HighScore();
        hsItem.playername = NameField.text;
        hsItem.score = score;
        Debug.Log("PostGameResults button clicked: " + NameField.text + " with scores " + score);
        Debug.Log("hsItem: " + JsonUtility.ToJson(hsItem));
        // StartCoroutine(PostRequestForHighScores(urlBackendHighScores, hsItem));
        ScoreButton.SetActive(false);
    }

    IEnumerator PostRequestForHighScores(string uri, HighScore hsItem)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(uri, JsonUtility.ToJson(hsItem)))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Received(UTF8): " + resultStr);
            }
        }
    }
}

