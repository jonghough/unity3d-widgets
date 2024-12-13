using UnityEngine;
using System.Collections;
using System;

public class DateManager
{

	private DateTime _dateTime;
	public DateManager(DateTime dateTime) { }


	public static int GetDaysInMonth(int year, int month)
	{
		return DateTime.DaysInMonth(year, month);
	}

	public static DayOfWeek GetFirstDayOfMonth(int year, int month)
	{
		DateTime dt = new DateTime(year, month, 1);
		return dt.DayOfWeek;
	}


}
