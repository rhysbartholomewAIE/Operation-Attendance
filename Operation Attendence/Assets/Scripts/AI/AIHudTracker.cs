using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHudTracker : MonoBehaviour
{
    public GameObject hudElementPrefab;
    private GameObject playerHud;

    [HideInInspector]
    public string aiName;
    public string aiScore;
    private HUDElement hudElement;

    private void Start()
    {
        playerHud = Instantiate(hudElementPrefab, this.transform);
        playerHud.GetComponent<Canvas>().worldCamera = Camera.main;
        hudElement = playerHud.GetComponentInChildren<HUDElement>();
        hudElement.nameText.text = aiName + " - " + aiScore;
    }

    private void Update()
    {
        playerHud.transform.LookAt(new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z));
    }

    public void UpdateScoreText(int newScore)
    {
        int i = int.Parse(aiScore);
        i += newScore;
        aiScore = i.ToString();
        hudElement.nameText.text = aiName + " - " + aiScore;
    }
}
