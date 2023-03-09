using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;

public class AttendancePanel : MonoBehaviour
{
    public TextMeshProUGUI headingText;
    public Transform namesHolder;
    public GameObject nameObjectToInstantiate;

    bool hasCreatedNames;

    List<AttendeeInfo> attendees = new List<AttendeeInfo>();
    AttendanceManager attendanceManager;

    private void Start()
    {
        attendanceManager = FindObjectOfType<AttendanceManager>();
    }

    public void OpenedPanel(DayInfo dayInfo)
    {
       
        GetComponentInChildren<Scrollbar>().value = 1;
        DateTime dateTime = new DateTime(dayInfo.dateTime.Year, dayInfo.dateTime.Month, dayInfo.dayNumber);
        string dayOfWeek = dateTime.DayOfWeek.ToString();
        headingText.text = dayOfWeek + " - " + dateTime.ToShortDateString();

        if (!hasCreatedNames)
        {

            DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/CharacterData/");
            FileInfo[] studentData = dir.GetFiles("*.*");

            foreach (FileInfo student in studentData)
            {
                if (student.FullName.Contains("meta"))
                {
                    continue;
                }
                GameObject go = Instantiate(nameObjectToInstantiate, namesHolder);
                string name = student.Name.Replace(".txt", "");
                go.GetComponentInChildren<TextMeshProUGUI>().text = name;
                go.GetComponent<AttendeeInfo>().SetName(name);
                attendees.Add(go.GetComponent<AttendeeInfo>());
            }

            hasCreatedNames = true;
        }
    }

    public void ConfirmAttendancePressed()
    {
        attendanceManager.ConfirmAttendance(attendees);
    }
}
