using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TankBattle
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TanksLabel;
        [SerializeField] private TextMeshProUGUI SitesLabel;
        private GameManager gm { get { return GameManager.Instance; }}

        public void UpdateLabels() {
            Level level = gm.CurrentLevel();
            int totalEnemies = level.NumberOfEnemies();
            SitesLabel.SetText("Enemy Sites Destroyed: " + level.State.SitesDestroyed + "/" + totalEnemies);
            TanksLabel.SetText("Enemy Tanks Destroyed: " + level.State.TanksDestroyed + "/" + totalEnemies);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void Show() {
            gameObject.SetActive(true);
        }
    }
}