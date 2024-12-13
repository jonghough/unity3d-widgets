using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class CalendarTitleController : MonoBehaviour
{
	[SerializeField]
	private Text _text;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetText(string text)
	{
		_text.text = text;
	}
}
