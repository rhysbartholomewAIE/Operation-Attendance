using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSpawner : MonoBehaviour
{
    public List<GameObject> characterOptions;
    public Collider spawnPoint;

    ProgressionManager progressionManager;

    Leaderboard leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        leaderboard = FindObjectOfType<Leaderboard>();

        Directory.CreateDirectory(Application.streamingAssetsPath + "/CharacterData/");
        progressionManager = FindObjectOfType<ProgressionManager>();

        if (PlayerPrefs.HasKey("Names Entered"))
        {
            Invoke("SpawnCharacters", 0.1f);
        }
    }

    public void CreateScriptableObjects(string[] names)
    {    
        if (progressionManager.hasEnteredClassDetails == true && progressionManager.hasCreatedStudents == false)
        {
            foreach (string name in names)
            {
                if (name != "")
                {
                    string location = Application.streamingAssetsPath + "/CharacterData/" + name + ".txt";

                    int characterNumber = UnityEngine.Random.Range(0, characterOptions.Count);
                    GameObject model = characterOptions[characterNumber];
                    File.AppendAllText(location, name);
                    File.AppendAllText(location, "\n" + model.name);
                    File.AppendAllText(location, "\n" + "0");

                    characterOptions.Remove(model);
                }
            }

            progressionManager.hasCreatedStudents = true;
            PlayerPrefs.SetString("Characters Created", "HasCreatedCharacters");
            PlayerPrefs.Save();
            
            SpawnCharacters();
        }
    }

    public void SpawnCharacters() 
    {
        if (progressionManager.hasEnteredClassDetails == true && progressionManager.hasCreatedStudents == true)
        {
            DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath +"/CharacterData/");
            FileInfo[] studentData = dir.GetFiles("*.*");
           
            foreach (FileInfo student in studentData)
            {               
                if (student.FullName.Contains("meta"))
                {
                    continue;
                }

                List<string> lines = new List<string>();

                using (var sr = new StreamReader(student.FullName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                Vector3 positionToSpawn = new Vector3(UnityEngine.Random.Range(spawnPoint.bounds.min.x, spawnPoint.bounds.max.x),0,UnityEngine.Random.Range(spawnPoint.bounds.min.z, spawnPoint.bounds.max.z));
                UnityEngine.Object go = Instantiate(Resources.Load("Characters/" + lines[1]), positionToSpawn, transform.rotation, this.transform);

                go.GetComponent<CharacterInfo>().studentName = lines[0];
                go.GetComponent<CharacterInfo>().characterModel = Resources.Load("Characters/" + lines[1]);               
                go.GetComponent<CharacterInfo>().score = int.Parse(lines[2]);

                go.GetComponent<AIHudTracker>().aiName = lines[0];
                go.GetComponent<AIHudTracker>().aiScore = lines[2];
                go.name = lines[0];
            }  
        }
        leaderboard.SortHighScores();
    }
}

