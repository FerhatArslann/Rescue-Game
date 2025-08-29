using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
	public static StoryController Instance;  // Singleton instance
	public TextMeshProUGUI m_Object;

	void Awake()
	{
		// Ensure only one instance of StoryController exists
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		Story();
	}

	void Story()
	{
		int num = Random.Range(1, 3);

		switch (num)
		{
			//1, 13:55 and 15:50
			case 1:
				m_Object.text = "We got a report at 13:55 about a missing child in the nearby woods. The clock is 15:50. Take everything you need from the storage and find that child.";
				break;
			//2, 22:30 and 04:52
			case 2:
				m_Object.text = "We got a report at 22:30 about a missing child in the nearby woods. The clock is 04:52. Take everything you need from the storage and find that child.";
				break;
		}
	}
}
