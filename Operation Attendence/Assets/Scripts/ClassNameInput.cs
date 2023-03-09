using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassNameInput : MonoBehaviour
{
    public GameObject classNameInputPrefab;
    public TMP_InputField inputField;

    public GameObject buttons;
    public GameObject inputFieldParent;

    int numberToSpawn;

    List<GameObject> nameInputFields = new List<GameObject>();
    public List<string> nameList = new List<string>();
    ProgressionManager progressionManager;
    UIManager uiManager;
    CharacterSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        progressionManager = FindObjectOfType<ProgressionManager>();
        uiManager = FindObjectOfType<UIManager>();
        spawner = FindObjectOfType<CharacterSpawner>();
        //inputField = GetComponentInChildren<TMP_InputField>();
    }


    public void SpawnNameInputs()
    {
        numberToSpawn = int.Parse(inputField.text);

        if(numberToSpawn > 0)
        {
            for(int i = 0; i < numberToSpawn; i++)
            {
                GameObject go = Instantiate(classNameInputPrefab, this.transform);
                nameInputFields.Add(go);
            }
        }
        
        inputFieldParent.SetActive(false);
        buttons.SetActive(true);
    }

    public void ConfirmNames()
    {
        int fieldCount = 0;
        nameList.Clear();
        foreach (GameObject go in nameInputFields)
        {
            if (go.GetComponent<TMP_InputField>().text != "")
            {
                fieldCount++;
                nameList.Add(go.GetComponent<TMP_InputField>().text);
            }
        }
        if (fieldCount >= numberToSpawn)
        {
            progressionManager.hasEnteredClassDetails = true;

            PlayerPrefs.SetString("Names Entered", "HasInputNames");
            PlayerPrefs.Save();
            
            progressionManager.SaveClassNames(nameList);
            progressionManager.hasEnteredClassDetails = true;

            spawner.CreateScriptableObjects(nameList.ToArray());
            uiManager.CloseClassDetails();
        }
        
    }
    public void ResetField()
    {
        foreach(GameObject go in nameInputFields)
        {
            Destroy(go);
        }
        nameInputFields.Clear();
        inputFieldParent.SetActive(true);
        buttons.SetActive(false);
    }
}


