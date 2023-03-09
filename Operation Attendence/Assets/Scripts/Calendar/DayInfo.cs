using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayInfo : MonoBehaviour
{
    public int dayNumber;
    public DateTime dateTime;
    
    public void SetInfo(int day, DateTime date)
    {
        dayNumber = day;
        dateTime = date;
    }
}
