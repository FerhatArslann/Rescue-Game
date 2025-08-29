using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNav : MonoBehaviour
{
    private bool isReportStage = true;

    // Update is called once per frame
    void Update()
    {
        if (isReportStage && Input.anyKeyDown)
        {
            GameObject.Find("Report").SetActive(false);
            isReportStage = false;
        }
    }
}
