using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiController : MonoBehaviour
{
    public TMP_Text tasklist;
    public TMP_Text uniform;
    public GameObject player;
    public GameObject target;
    public TMP_Text holdbtn;
    private string spriteName;
    public SpriteRenderer spriteRenderer;
    public Sprite FFfulprofile;
    public Sprite SARfulprofile;
    public GameObject uniformDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //tasklist = GetComponent<TMPro.TextMeshProUGUI>();
        // uniform = GetComponent<TMPro.TextMeshProUGUI>();

    }

    void SpriteDetect()
    {
        spriteName = player.GetComponent<SpriteRenderer>().sprite.ToString();
        if (spriteName[0] == 'f')
        {
            uniform.text = "Firefighter";
            uniformDisplay.GetComponent<Image>().sprite = FFfulprofile;
        }

        if (spriteName[0] == 'S')
        {
            uniform.text = "Search 'n' Rescue";
            uniformDisplay.GetComponent<Image>().sprite = SARfulprofile;
        }

    }
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            holdbtn.text = "Game paused";
            Time.timeScale = 0;
        }
    }
   public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            holdbtn.text = "";
            Time.timeScale = 1;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartLevel(string NextLevel)
    {
        SceneManager.LoadScene(NextLevel, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) tasklist.text = "none";
        else
        {
            tasklist.text = "find the missing person";
        }
        if (player == null) uniform.text = "...";
        else
        {
            SpriteDetect();
        }


    }
}
