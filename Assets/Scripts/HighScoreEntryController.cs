using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreEntryController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI positionLabel;
    [SerializeField] private TextMeshProUGUI scoreSitesLabel;
    [SerializeField] private TextMeshProUGUI scoreTanksLabel;
    [SerializeField] private TextMeshProUGUI scoreDeathsLabel;

    public void SetValues(int position, int scoreSites, int scoreTanks, int scoreDeaths) {
        positionLabel.SetText($"{position}.");
        scoreSitesLabel.SetText($"Sites: {scoreSites}");
        scoreTanksLabel.SetText($"Tanks: {scoreTanks}");
        scoreDeathsLabel.SetText($"Deaths: {scoreDeaths}");
    }
}
