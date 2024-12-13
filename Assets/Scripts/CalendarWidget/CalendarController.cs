using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CalendarController
{

	private int _year, _month;

	public CalendarController(int year, int month)
	{
		_year = year;
		_month = month;
	}

	public DayOfWeek GetFirstDayOfMonth()
	{
		return DateManager.GetFirstDayOfMonth(_year, _month);
	}

	public int GetNumberOfDaysInMonth()
	{
		return DateManager.GetDaysInMonth(_year, _month);
	}



	public void InsertData(List<CalendarItemController> daysList, Color dayColor, Color weekendColor, Color otherMonthColor)
	{
		int firstDay = (int)GetFirstDayOfMonth();

		DateTime dt = new DateTime(_year, _month, 1);
		var prevMonthDT = dt.AddMonths(-1); // previous month 
		var nextMonthDT = dt.AddMonths(1); // next month
		int daysInPreviousMonth = DateManager.GetDaysInMonth(prevMonthDT.Year, prevMonthDT.Month);

		for (int i = 0; i < daysList.Count; i++)
		{
			if (i < (int)GetFirstDayOfMonth())
			{
				daysList[i].SetDate(prevMonthDT.Year, prevMonthDT.Month, daysInPreviousMonth - firstDay + i + 1);
				daysList[i].SetColor(otherMonthColor);
			}
			else if (i <= GetNumberOfDaysInMonth() + firstDay - 1)
			{
				daysList[i].SetDate(_year, _month, i + 1 - firstDay);
				daysList[i].SetColor(dayColor);
			}
			else
			{
				daysList[i].SetDate(nextMonthDT.Year, nextMonthDT.Month, i + 1 - GetNumberOfDaysInMonth() - firstDay);
				daysList[i].SetColor(otherMonthColor);
			}
		}
	}
}
