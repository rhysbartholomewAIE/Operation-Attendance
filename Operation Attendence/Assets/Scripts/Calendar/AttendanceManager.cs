using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttendanceManager : MonoBehaviour
{
    UIManager uiManager;
    AttendancePanel attendancePanel;
    Leaderboard leaderboard;
    public int scoreToAdd;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        leaderboard = FindObjectOfType<Leaderboard>();
    }

    public void DayClicked(GameObject button)
    {
        uiManager.OpenAttendance();
        attendancePanel = FindObjectOfType<AttendancePanel>();
        DayInfo dayInfo = button.GetComponent<DayInfo>();
        attendancePanel.OpenedPanel(dayInfo);
    }

    public void ConfirmAttendance(List<AttendeeInfo> attendeeInfo)
    {
        Debug.Log(attendeeInfo.Count);
        CharacterInfo[] students = Resources.FindObjectsOfTypeAll<CharacterInfo>();

        foreach (AttendeeInfo attendance in attendeeInfo)
        {
            if (attendance.hasAttended)
            {
                foreach (CharacterInfo s in students)
                {
                    Debug.Log(s.studentName + "    " + attendance.studentName);
                    if(s.studentName == attendance.studentName)
                    {
                        Debug.Log(s.studentName);
                        s.gameObject.GetComponent<CharacterInfo>().ChangeScore(scoreToAdd);
                        s.gameObject.GetComponent<AIHudTracker>().UpdateScoreText(scoreToAdd);
                    }
                } 
            }
        }

        leaderboard.SortHighScores();
       // uiManager.CloseAttendance();
    }
}
