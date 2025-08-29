using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBT : MonoBehaviour
{
    [SerializeField] private string newScene = "Level_1";

    public void NewSceneBT()
    {
        SceneManager.LoadScene(newScene);
    }
}
