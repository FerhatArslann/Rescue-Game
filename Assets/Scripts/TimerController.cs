using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets;

public class TimerController : MonoBehaviour
{
    public TMP_Text tmpTxtTimer;
    private int timer;
    private int time;
    public GameObject player;

    public GameObject destination;
    float interval = 1.0f;
    //GameObject player = GameObject.Find("Player");
    //GameObject civilian = GameObject.Find("Destination");
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("UpdateLevel");
    }

    IEnumerator UpdateLevel()
    {
        
        for (; ; )
        {
            timer += 1;
            yield return new WaitForSeconds(interval);
            tmpTxtTimer.text = timer.ToString();
        }
    }

    int Score()
    {

        //vanha
        //float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, GameObject.FindGameObjectWithTag("Destination").transform.position);

        //uusi
        float distance = Vector3.Distance(player.transform.position, destination.transform.position);
        if (distance < 3)
        {
            time = - timer + 360;
            SceneManager.LoadScene("EndScene");
            Debug.Log("Calculated Score is: " + time);
            Globals.currentTime = timer;
            return time;
        }
        else
        {
            return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }
}
