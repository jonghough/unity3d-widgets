using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class MonthButtonController : MonoBehaviour
{
	[SerializeField]
	private Text _text;

	private Action _onClick;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}


	public void Setup(string text, Action clickCallback)
	{
		_text.text = text;
		_onClick = clickCallback;
	}

	public void OnClick()
	{
		if (_onClick != null)
			_onClick();
	}
}
