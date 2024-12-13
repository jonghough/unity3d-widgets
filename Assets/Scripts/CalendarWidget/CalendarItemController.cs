using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class CalendarItemController : MonoBehaviour
{

	private bool _isHeader = false;
	private int _date;
	private int _month;
	private int _year;
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


	public void SetDate(int year, int month, int day)
	{
		_date = day;
		_month = month;
		_year = year;
		_text.text = "" + day;
	}

	public void SetHeader(string text, Color _headerColor)
	{
		_isHeader = true;
		_text.text = text;
		this.GetComponent<Image>().color = _headerColor;
	}

	public void SetNotHeader()
	{
		_isHeader = false;
	}


	public void SetColor(Color color)
	{
		GetComponent<Image>().color = color;
	}

	public void OnClickAction()
	{
		Debug.Log("Clicked :: " + this._text.text + " / " + _month + " / " + _year);
	}
}
