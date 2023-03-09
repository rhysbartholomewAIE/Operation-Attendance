using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<Panel> panels;
    //Panel[] panelsList;
    public GameObject panelParent;
    void Awake()
    {
        foreach(Transform child in panelParent.transform)
        {
            panels.Add(child.GetComponent<Panel>());
        }

        foreach(Panel panel in panels)
        {
            if(panel.panelName != Panel.PanelNames.MainPanel)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
    public void OpenCalendar()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.CalendarPanel)
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void CloseCalendar()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.CalendarPanel)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }

    public void EnterClassDetails()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.EnterClassDetails)
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void CloseClassDetails()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.EnterClassDetails)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }

    public void OpenSettings()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.Settings)
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void CloseSettings()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.Settings)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
    public void OpenAttendance()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.AttendancePanel)
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void CloseAttendance()
    {
        foreach (Panel panel in panels)
        {
            if (panel.panelName == Panel.PanelNames.AttendancePanel)
            {
                panel.gameObject.SetActive(false);
            }
        }
    }

}
