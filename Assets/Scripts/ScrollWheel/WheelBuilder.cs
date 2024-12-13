using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class WheelBuilder : MonoBehaviour
{

	public GameObject originalPrefab;

	public ScrollWheel scrollWheel;

	// Use this for initialization
	void Start()
	{
		List<RectTransform> l = new List<RectTransform>();

		for (int i = 0; i < 10; i++)
		{
			GameObject go = GameObject.Instantiate(originalPrefab) as GameObject;
			l.Add(go.GetComponent<RectTransform>());
		}
		scrollWheel.Setup(l);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
