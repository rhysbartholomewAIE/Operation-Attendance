using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public string studentName;
    public Object characterModel;
    public int score;

    public void ChangeScore(int scoreToAdd)
    {
        score += scoreToAdd;

        string location = Application.streamingAssetsPath + "/CharacterData/" + name + ".txt";
        string[] stringToWrite = {studentName, characterModel.name, score.ToString() };

        File.WriteAllLines(location, stringToWrite);
    }
}
