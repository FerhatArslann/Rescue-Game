using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DaytimeController : MonoBehaviour
{
	public SpriteRenderer lightObject;

	void Start()
	{
		SetLightAlpha();
	}

	void SetLightAlpha()
	{
        // Access the StoryController instance to get the text
        TextMeshProUGUI storyText = StoryController.Instance.m_Object;

        // Check the content of the story text and set alpha accordingly
        if (storyText.text.Contains("15:50"))
		{
			SetAlpha(0); // Day
		}
		else if (storyText.text.Contains("04:52"))
		{
			SetAlpha(240); // Night
		}
		else
		{
			SetAlpha(0);
		}
	}

	void SetAlpha(int alphaValue)
	{
		// Set alpha value for the sprite object
		Color color = lightObject.color;
		color.a = alphaValue / 255f;
		lightObject.color = color;
	}
}
