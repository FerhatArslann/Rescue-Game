using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class MainController : MonoBehaviour
{
    public TMP_InputField tmpIfBestTime;
    public TMP_Text highScoresTextArea;
    public GameObject graySquare;

    bool updateHighScoreTextArea = false;
    const string urlBackendHighScores = "https://example.com/database/gameWithPHP/api/highscores.php";
    const string urlBackendHighScoresFile = "https://example.com/database/gameWithPHP/highscores.json";

    HighScores.HighScores hs;

    //public string NextLevel = "Story Scene";
    //public GameObject theScreen;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    void Update()
    {
        if (updateHighScoreTextArea)
        {
            highScoresTextArea.text = CreateHighScoreList();
            updateHighScoreTextArea = false;
        }
    }

    private void UpdateScore()
    {
        FetchHighScoresJSONFile();
        FetchHighScoresJSON();
    }

    string CreateHighScoreList()
    {
        string hsList = "";
        if (hs != null)
        {
            int len = (hs.scores.Length < 3) ? hs.scores.Length : 3;
            for (int i = 0; i < len; i++)
            {
                hsList += string.Format("[{0}] {1,15} {2,5}\n {3,8}\n",
                    (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
            }
        }
        return hsList;
    }

    IEnumerator GetRequestForHighScores(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // set downloadHandler for json
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");
        // Request and wait for reply
        yield return webRequest.SendWebRequest();
        // get raw data and convert it to string
        string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            // create HighScore item from json string
            hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
            updateHighScoreTextArea = true;
            Debug.Log("Received(UTF8): " + resultStr);
            Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
            Debug.Log("Received(HS) name: " + hs.scores[0].playername);
        }
    }

    public void FetchHighScoresJSONFile()
    {
        Debug.Log("FetchHighScoresJSONFile button clicked");
        // StartCoroutine(GetRequestForHighScoresFile(urlBackendHighScoresFile));
    }
    public void FetchHighScoresJSON()
    {
        Debug.Log("FetchHighScoresJSON button clicked");
        // StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }

    IEnumerator GetRequestForHighScoresFile(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // set downloadHandler for json
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");
        // Request and wait for reply
        yield return webRequest.SendWebRequest();
        // get raw data and convert it to string
        string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            // create HighScore item from json string
            hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
            Debug.Log("Received(UTF8): " + resultStr);
            Debug.Log("First item:" + hs.scores[0].playername + " score: " + hs.scores[0].score);
            Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
        }
    }

    public void Infomenu()
    {
        if (graySquare.activeSelf == true) {
            graySquare.SetActive(false);
        }
        else
        {
            graySquare.SetActive(true);
        }
    }

    public void StartLevel(string NextLevel)
    {
        SceneManager.LoadScene(NextLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
