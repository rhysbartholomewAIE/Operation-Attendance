using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public enum PanelNames
    {
        MainPanel,
        CalendarPanel,
        EnterClassDetails,
        Settings,
        AttendancePanel
    }
    public PanelNames panelName;
}
