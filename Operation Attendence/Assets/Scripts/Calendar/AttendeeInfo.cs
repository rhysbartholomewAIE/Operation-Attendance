using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendeeInfo : MonoBehaviour
{
    public string studentName;
    public bool hasAttended;
  
    public void SetName(string name)
    {
        studentName = name;
    }
    public void SetAttendance(bool hasAttendedCheck)
    {
        hasAttended = hasAttendedCheck;
    }
}
