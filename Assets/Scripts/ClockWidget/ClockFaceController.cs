using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ClockFaceController : MonoBehaviour
{

	[SerializeField]
	private Image _hourHand;
	[SerializeField]
	private Image _minuteHand;
	[SerializeField]
	private Image _secondHand;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(UpdateTime());

	}

	IEnumerator UpdateTime()
	{
		while (true)
		{
			Setup();
			yield return new WaitForSeconds(0.001f);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void Setup()
	{
		Vector3[] parentDim = new Vector3[4];
		this.GetComponent<RectTransform>().GetLocalCorners(parentDim);
		//		float pWidth = Mathf.Abs (parentDim [2].x - parentDim [0].x);
		//		float pHeight = Mathf.Abs (parentDim [1].y - parentDim [0].y);
		//		float pXCenter = 0.5f * (parentDim [2].x + parentDim [0].x);
		//		float pYCenter = 0.5f * (parentDim [1].y + parentDim [0].y);

		DateTime dt = DateTime.Now;

		float hourAngle = GetAngleForHour(dt.Hour, dt.Minute);
		float minuteAngle = GetAngleForMinute(dt.Minute, dt.Second);
		float secondAngle = GetAngleForSecond(dt.Second);


		RectTransform hourR = _hourHand.GetComponent<RectTransform>();
		RectTransform minuteR = _minuteHand.GetComponent<RectTransform>();
		RectTransform secondR = _secondHand.GetComponent<RectTransform>();

		Vector3 hEA = hourR.eulerAngles;
		hEA.z = hourAngle;
		hourR.eulerAngles = hEA;

		Vector3 mEA = minuteR.eulerAngles;
		mEA.z = minuteAngle;
		minuteR.eulerAngles = mEA;

		Vector3 sEA = minuteR.eulerAngles;
		sEA.z = secondAngle;
		secondR.eulerAngles = sEA;


		hourR.anchoredPosition = new Vector2(0, 0);
		minuteR.anchoredPosition = new Vector2(0, 0);
		secondR.anchoredPosition = new Vector2(0, 0);
	}


	public float GetAngleForHour(int hour, int minute)
	{
		int t = hour % 12;

		float tInterpolated = t + minute * 1f / 60;

		return -360f * tInterpolated * 1f / 12;
	}

	public float GetAngleForMinute(int minute, int second)
	{
		int t = minute % 60;

		float tInterpolated = t + second * 1f / 60;

		return -360f * tInterpolated * 1f / 60;
	}

	public float GetAngleForSecond(int second)
	{
		int s = second % 60;
		return -360f * s * 1f / 60;
	}




}
