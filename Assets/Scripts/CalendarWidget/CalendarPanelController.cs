using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(RectTransform))]
public class CalendarPanelController : MonoBehaviour
{


	//dimensions.
	private List<CalendarItemController> _calendarItems = new List<CalendarItemController>();
	private Vector3[] _parentDim = new Vector3[4];
	private Vector2 _currentItemPosition = new Vector2(0, 0);

	[SerializeField]
	private float _headerOffset;
	private CalendarController _cc;

	[SerializeField]
	private int _year;
	[SerializeField]
	private int _month;

	[SerializeField]
	private Color _dayColor;

	[SerializeField]
	private Color _headerColor;

	[SerializeField]
	private Color _weekendColor;

	[SerializeField]
	private Color _otherMonthColor;

	void Start()
	{
		Setup(2024, 1);
	}

	private void Setup(int year, int month)
	{
		_year = year;
		_month = month;
		_calendarItems.Clear();
		_cc = new CalendarController(year, month);
		System.DayOfWeek dow = _cc.GetFirstDayOfMonth();

		int daysInMonth = _cc.GetNumberOfDaysInMonth();
		this.GetComponent<RectTransform>().GetLocalCorners(_parentDim);
		float _pWidth = Mathf.Abs(_parentDim[2].x - _parentDim[0].x);
		float _pHeight = Mathf.Abs(_parentDim[1].y - _parentDim[0].y);

		GameObject DataPanal = Resources.Load<GameObject>("Prefabs/CalendarWidget/DataPanel");
		Vector3[] itemDim = new Vector3[4];
		DataPanal.GetComponent<RectTransform>().GetLocalCorners(itemDim);

		GameObject MonthButton = Resources.Load<GameObject>("Prefabs/CalendarWidget/MonthButton");
		Vector3[] monthButtonDim = new Vector3[4];
		MonthButton.GetComponent<RectTransform>().GetLocalCorners(monthButtonDim);

		GameObject Title = Resources.Load<GameObject>("Prefabs/CalendarWidget/TitlePanel");
		Vector3[] titlePanelDim = new Vector3[4];
		Title.GetComponent<RectTransform>().GetLocalCorners(titlePanelDim);

		///
		///		Title and change month buttons.
		///

		for (int b = 0; b < 3; b++)
		{
			if (b == 1)
			{

				GameObject pan = Instantiate(Title);
				RectTransform rt = pan.GetComponent<RectTransform>();
				pan.GetComponent<RectTransform>().SetParent(transform);
				float w = _pWidth / 7;
				rt.anchoredPosition = new Vector2(_pWidth * 0.5f, -GetItemHeight(titlePanelDim) * 0.5f);
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GetItemWidth(titlePanelDim));
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetItemHeight(titlePanelDim));
				rt.localScale = new Vector3(1, 1, 1);
				pan.GetComponent<CalendarTitleController>().SetText(_month + "/" + _year);
				Vector3 pos = rt.localPosition;
				pos.z = 0;
				rt.localPosition = pos;

			}
			else
			{

				GameObject pan = Instantiate(MonthButton);
				RectTransform rt = pan.GetComponent<RectTransform>();
				pan.GetComponent<RectTransform>().SetParent(transform);
				int dir = b == 0 ? -1 : 1;
				rt.anchoredPosition = new Vector2(_pWidth * 0.5f + dir * (GetItemWidth(titlePanelDim) * 0.5f + 0.5f * GetItemWidth(monthButtonDim)), -GetItemHeight(titlePanelDim) * 0.5f);
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GetItemWidth(monthButtonDim));
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetItemHeight(monthButtonDim));
				rt.localScale = new Vector3(1, 1, 1);

				Vector3 pos = rt.localPosition;
				pos.z = 0;
				rt.localPosition = pos;

				if (b == 2)
				{
					pan.GetComponent<MonthButtonController>().Setup(">", () =>
					{
						foreach (Transform t in this.transform)
							Destroy(t.gameObject);
						DateTime dt = new DateTime(_year, _month, 1);
						dt = dt.AddMonths(1);
						Setup(dt.Year, dt.Month);
					});
				}
				else
				{
					pan.GetComponent<MonthButtonController>().Setup("<", () =>
					{
						foreach (Transform t in this.transform)
							Destroy(t.gameObject);
						DateTime dt = new DateTime(_year, _month, 1);
						dt = dt.AddMonths(-1);
						Setup(dt.Year, dt.Month);
					});
				}

			}
		}

		///
		///    Calendar header panels.
		///

		for (int i = 0; i < 7; i++)
		{
			GameObject pan = Instantiate(DataPanal);
			RectTransform rt = pan.GetComponent<RectTransform>();
			pan.GetComponent<RectTransform>().SetParent(transform);
			float w = _pWidth / 7;
			float h = (_pHeight - _headerOffset) / 7;
			rt.anchoredPosition = new Vector2(w * i + w * 0.5f, -_headerOffset - h * 0.5f);
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
			rt.localScale = new Vector3(1, 1, 1);

			Vector3 pos = rt.localPosition;
			pos.z = 0;
			rt.localPosition = pos;
			pan.GetComponent<CalendarItemController>().SetHeader(
					System.Enum.GetName(typeof(System.DayOfWeek), i).Substring(0, 2).ToUpper(),
					_headerColor);
		}


		///
		///		Calendar day item panels.
		///

		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++)
			{

				GameObject pan = Instantiate(DataPanal);
				RectTransform rt = pan.GetComponent<RectTransform>();
				pan.GetComponent<RectTransform>().SetParent(transform);
				float w = _pWidth / 7;
				float h = (_pHeight - _headerOffset) / 7;
				rt.anchoredPosition = new Vector2(w * j + w * 0.5f, -_headerOffset - h * (i + 1) - h * 0.5f);
				_calendarItems.Add(pan.GetComponent<CalendarItemController>());
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
				rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
				rt.localScale = new Vector3(1, 1, 1);

				Vector3 pos = rt.localPosition;
				pos.z = 0;
				rt.localPosition = pos;
			}

			if (daysInMonth <= 7 * (i + 1) - (int)dow)
				break;
		}

		_cc.InsertData(_calendarItems, _dayColor, _weekendColor, _otherMonthColor);
	}

	/// <summary>
	/// Gets the height of the item.
	/// </summary>
	/// <returns>The item height.</returns>
	/// <param name="dimensions">Dimensions.</param>
	private float GetItemHeight(Vector3[] dimensions)
	{
		return Mathf.Abs(dimensions[1].y - dimensions[0].y);
	}


	/// <summary>
	/// Gets the width of the item.
	/// </summary>
	/// <returns>The item width.</returns>
	/// <param name="dimensions">Dimensions.</param>
	private float GetItemWidth(Vector3[] dimensions)
	{
		return Mathf.Abs(dimensions[2].x - dimensions[0].x);
	}

}
