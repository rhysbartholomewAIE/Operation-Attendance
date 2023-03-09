using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{

    public bool hasEnteredClassDetails;
    public bool hasCreatedStudents;
    private UIManager UImanager;

    string savePath;
    string classNamesPath;

    // Start is called before the first frame update
    void Start()
    {
        UImanager = FindObjectOfType<UIManager>();

        hasEnteredClassDetails = PlayerPrefs.HasKey("Names Entered");
        hasCreatedStudents = PlayerPrefs.HasKey("Characters Created");

        if (!hasEnteredClassDetails)
        {
            UImanager.EnterClassDetails();
        }
       
    }

    public void SaveClassNames(List<string> names)
    {
        int counter = 0;
        foreach(string name in names)
        {
            PlayerPrefs.SetString("character name" + counter.ToString(), name);
            PlayerPrefs.Save();
        }

    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/CharacterData/");
        FileInfo[] studentData = dir.GetFiles("*.*");

        foreach(FileInfo studentDataFile in studentData)
        {
            studentDataFile.Delete();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
