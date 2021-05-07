using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoresController : MonoBehaviour
{
    [SerializeField] private HighScoreEntryController highScoreEntry;
    [SerializeField] private TextMeshProUGUI noScoresLabel;
    
    private void OnEnable() {
        ClearList();

        HighScore[] scores = HighScoresManager.LoadScores();
        bool hasScores = scores.Length > 0;
        noScoresLabel.gameObject.SetActive(!hasScores);
        
        for(int i = 0; i < scores.Length; i++ ){
            CreateEntry(i, scores[i]);
        }
    }

    private void CreateEntry(int index, HighScore score) {
        HighScoreEntryController entry = GameObject.Instantiate(highScoreEntry, this.transform, false);
        Vector3 position = entry.transform.position;
        position.y -= index * 100;
        entry.transform.position = position;
        entry.SetValues(index + 1, score.sitesDestroyed, score.tanksDestroyed, score.numberOfDeaths);
    }

    private void ClearList(){
        List<GameObject> entries = new List<GameObject>();
        foreach(Transform child in transform) {
            if(child.gameObject.GetComponent<HighScoreEntryController>() != null) {
                entries.Add(child.gameObject);
            }
        }

        foreach (var entry in entries){ Destroy(entry); }
    }
}
