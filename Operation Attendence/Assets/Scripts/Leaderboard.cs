using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class Leaderboard : MonoBehaviour
{
    public class StudentData : IComparable<StudentData>
    {
        public int score;
        public string name;

        public int CompareTo(StudentData data)
        {       // A null value means that this object is greater.
            if (data == null)
            {
                Leaderboard leaderboard = FindObjectOfType<Leaderboard>();
                return leaderboard.students.Count;
            }
            else
            {
                return -this.score.CompareTo(data.score);
            }
        }
    }

    public GameObject leaderboardTextHolder;
    public GameObject leaderboardTextItem;
    public List<StudentData> students = new List<StudentData>();

    public int numberToDisplay;
    public List<GameObject> leaderBoardItems;
    public void SortHighScores()
    {
        students.Clear();
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/CharacterData/");
        FileInfo[] studentData = dir.GetFiles("*.*");

        //Transferring each student text file to a class to add to list
        foreach (FileInfo studentDataFile in studentData)
        {
            if (studentDataFile.FullName.Contains("meta"))
            {
                continue;
            }
            List<string> lines = new List<string>();

            using (var sr = new StreamReader(studentDataFile.FullName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            StudentData student = new StudentData();
            try
            {
                student.score = int.Parse(lines[2]);
            }
            catch
            {
                Debug.Log("not an int");
            }
            student.name = lines[0];
            students.Add(student);
        }

        students.Sort();
        FillLeaderBoardTable();
    }

    void FillLeaderBoardTable()
    {
        foreach (GameObject go in leaderBoardItems)
        {           
                Destroy(go.gameObject);
        }
        leaderBoardItems.Clear();
            
        
        for (int i = 0; i < numberToDisplay; i++)
        {
            GameObject go = Instantiate(leaderboardTextItem, leaderboardTextHolder.transform);
            leaderBoardItems.Add(go);
            go.GetComponent<TextMeshProUGUI>().text = (i + 1) + ". " +students[i].name + " - " + students[i].score;
        }
    }
}
