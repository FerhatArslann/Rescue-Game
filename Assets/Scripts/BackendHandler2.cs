using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.VisualScripting;

namespace HighScores
{
    [System.Serializable]
    public class HighScores
    {
        public HighScore[] scores;
    }

    [System.Serializable]
    public class HighScore
    {
        public int id = 0;
        public string playername = "";
        public float score = 0.0f;
        public string playtime = "";
    }

    public class BackendHandler : MonoBehaviour
    {
        const string urlBackendHighScoresFile = "https://example.com/database/gameWithPHP/api/highscores.json";
        const string urlBackendHighScores = "https://example.com/database/gameWithPHP/api/highscores.php";

        public UnityEngine.UI.InputField playerNameInput;
        public UnityEngine.UI.InputField scoreInput;
        public UnityEngine.UI.Text highScoresTextArea;
        public UnityEngine.UI.Text logTextArea;

        private HighScores hs;
        private bool updateHighScoreTextArea = false;
        private int fetchCounter = 0;

        void Start()
        {
            Debug.Log("BackendHandler started");
            // StartCoroutine(GetRequestForHighScoresFile(urlBackendHighScoresFile));
        }

        void Update()
        {
            logTextArea.text = GetLog();
            if (updateHighScoreTextArea)
            {
                highScoresTextArea.text = CreateHighScoreList();
                updateHighScoreTextArea = false;
            }
        }

        public void PostGameResults()
        {
            fetchCounter++;
            Debug.Log("PostGameResults button clicked: " + playerNameInput.text + " with scores " + scoreInput.text);

            HighScore hsItem = new HighScore();
            hsItem.playername = playerNameInput.text;
            hsItem.score = float.Parse(scoreInput.text);
            hsItem.playtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // StartCoroutine(PostRequestForHighScores(urlBackendHighScores, hsItem));
        }

        public void FetchHighScoresJSON()
        {
            fetchCounter++;
            Debug.Log("FetchHighScoresJSON button clicked");
            // StartCoroutine(GetRequestForHighScores(urlBackendHighScores));
        }

        IEnumerator GetRequestForHighScoresFile(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                InsertToLog("Request sent to " + uri);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("Accept", "application/json");

                yield return webRequest.SendWebRequest();

                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    InsertToLog("Error encountered: " + webRequest.error);
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    hs = JsonUtility.FromJson<HighScores>(resultStr);
                    updateHighScoreTextArea = true;
                    InsertToLog("Response received successfully ");
                    Debug.Log("Received(UTF8): " + resultStr);
                    Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
                    Debug.Log("Received(HS) name: " + hs.scores[0].playername);
                }
            }
        }

        IEnumerator GetRequestForHighScores(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                InsertToLog("Request sent to " + uri);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("Accept", "application/json");

                yield return webRequest.SendWebRequest();

                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    InsertToLog("Error encountered: " + webRequest.error);
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    hs = JsonUtility.FromJson<HighScores>(resultStr);
                    updateHighScoreTextArea = true;
                    InsertToLog("Response received successfully ");
                    Debug.Log("Received(UTF8): " + resultStr);
                    Debug.Log("Received(HS): " + JsonUtility.ToJson(hs));
                    Debug.Log("Received(HS) name: " + hs.scores[0].playername);
                }
            }
        }

        IEnumerator PostRequestForHighScores(string uri, HighScore hsItem)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(uri, JsonUtility.ToJson(hsItem)))
            {
                InsertToLog("POST request sent to " + uri);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("Accept", "application/json");

                yield return webRequest.SendWebRequest();

                string resultStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    InsertToLog("Error encountered: " + webRequest.error);
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    InsertToLog("POST response received successfully ");
                    Debug.Log("Received(UTF8): " + resultStr);
                }
            }
        }

        string CreateHighScoreList()
        {
            string hsList = "";
            if (hs != null)
            {
                int len = (hs.scores.Length < 3) ? hs.scores.Length : 3;
                for (int i = 0; i < len; i++)
                {
                    hsList += string.Format("[ {0} ] {1,15} {2,5} {3,15} {4}\n",
                        (i + 1), hs.scores[i].playername, hs.scores[i].score, hs.scores[i].playtime);
                }
            }
            return hsList;
        }

        void InsertToLog(string message)
        {
            logTextArea.text += "\n" + fetchCounter + ": " + message;
        }

        string GetLog()
        {
            return logTextArea.text;
        }
    }
}