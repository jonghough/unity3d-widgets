using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ScrollWheelItem : MonoBehaviour
{

	public float currentAngle;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PerformAction()
	{
		Debug.Log("Object clicked!");
		// Add your custom action here
	}
}
