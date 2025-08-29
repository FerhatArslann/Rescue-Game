using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighScores;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class BackendHandler : MonoBehaviour
{
    public TMPro.TMP_Text highScoresTextArea;
    public TMPro.TMP_Text logTextArea;
    bool updateHighScoreTextArea = false;
    private int fetchCounter = 0;
    string log = "";
    const string urlBackendHighScoresFile = "https://example.com/database/gameWithPHP/api/highscores.json";
    const string urlBackendHighScores = "https://example.com/database/gameWithPHP/api/highscores.php";
    
    HighScores.HighScores hs;

    const string jsonTestStr = "{ " +
        "\"scores\":[ " +
        "{\"id\":1, \"playername\":\"Matti\", \"score\":20, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":2, \"playername\":\"Hankka\", \"score\":30, \"playtime\": \"2020-21-11 08:20:00\"}, " +
        "{\"id\":3, \"playername\":\"Ismo\", \"score\":40, \"playtime\": \"2020-21-11 08:20:00\"} " +
        "] }";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BackendHandler started");
        // conversion from JSON string to object
        hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
        Debug.Log("HighScores name: " + hs.scores[0].playername);
        // conversion from object to JSON string
        Debug.Log("HighScores as json: " + JsonUtility.ToJson(hs));
    }

    // Update is called once per frame
    void Update()
    {
        logTextArea.text = log;
        if (updateHighScoreTextArea)
        {
            highScoresTextArea.text = CreateHighScoreList(); updateHighScoreTextArea = false;
        }
    }
    string CreateHighScoreList()
    {
        string hsList = "";
        if (hs != null)
        {
            int len = (hs.scores.Length < 3) ? hs.scores.Length : 3; for (int i = 0; i < len; i++)
            {
                /* hsList += hs.scores[i].playername + ": \t" + hs.scores[i].score + " \t" + hs.scores[i].playtime+"\n"; */
                hsList += string.Format("[ {0} ] | {1,-15} | {2,5} | {3,-15}\n", (i + 1),
                    hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
            }
        }
        return hsList;
    }

    IEnumerator GetRequestForHighScores(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            InsertToLog("Request sent to " + uri);
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
                InsertToLog("Error encountered: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // create HighScore item from json string
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
                updateHighScoreTextArea = true;
                InsertToLog("Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr);
                Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
                Debug.Log("Received(HS) name: " + hs.scores[0].playername);
            }
        }
    }

    public void FetchHighScoresJSONFile()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSONFile button clicked");
        // StartCoroutine(GetRequestForHighScoresFile(urlBackendHighScoresFile));
    }
    public void FetchHighScoresJSON()
    {
        fetchCounter++;
        Debug.Log("FetchHighScoresJSON button clicked");
        // StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
    }
    string InsertToLog(string s) { return log = "[" + fetchCounter + "] " + s + "\n" + log; }
    string GetLog() { return log; }

    IEnumerator GetRequestForHighScoresFile(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            InsertToLog("Request sent to " + uri);
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
                InsertToLog("Error encountered: " + webRequest.error);
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // create HighScore item from json string
                hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
                InsertToLog("Response received succesfully ");
                Debug.Log("Received(UTF8): " + resultStr);
                Debug.Log("First item:" + hs.scores[0].playername + " score: " + hs.scores[0].score);
                Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
            }
        }
    }
    // :
    public TMPro.TMP_InputField playerNameInput;
    public TMPro.TMP_InputField scoreInput;
    public UnityEngine.UI.Button postGameResult;
    bool scoreInputsOk = false;
    public void PostGameResults()
    {
        checkScore();
        if (!scoreInputsOk) return;
        HighScore hsItem = new HighScore();
        hsItem.playername = playerNameInput.text;
        hsItem.score = int.Parse(scoreInput.text);
        Debug.Log("PostGameResults button clicked: " + playerNameInput.text + " with scores " + scoreInput.text);
        Debug.Log("hsItem: " + JsonUtility.ToJson(hsItem));
        // StartCoroutine(PostRequestForHighScores(urlBackendHighScores, hsItem));
    }
    IEnumerator PostRequestForHighScores(string uri, HighScore hsItem)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(uri, JsonUtility.ToJson(hsItem)))
        {
            InsertToLog("POST request sent to " + uri);
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
                InsertToLog("Error encountered in post request: " + webRequest.error);
                Debug.Log("Error in post request: " + webRequest.error);
            }
            else
            {
                InsertToLog("POST request succesful");
                Debug.Log("Received(UTF8): " + resultStr);
            }
        }
    }
    public void checkScore()
    {
        if (float.TryParse(scoreInput.text, out _) && (playerNameInput.text.Trim().Length > 0))
        {
            //postGameResult.enabled = true;
            scoreInputsOk = true;
        }
        else
        {
            //postGameResult.enabled = false;
            scoreInputsOk = false;
        }
    }
}