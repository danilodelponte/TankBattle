using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TankBattle {
    public class PauseMenu : MonoBehaviour
    {
        
        [SerializeField] private TextMeshProUGUI GameOverLabel;
        [SerializeField] private TextMeshProUGUI HighScoreLabel;
        [SerializeField] private TextMeshProUGUI SitesDestroyedLabel;
        [SerializeField] private TextMeshProUGUI TanksDestroyedLabel;
        [SerializeField] private TextMeshProUGUI DeathsLabel;
        [SerializeField] private Button NextLevelButton;
        [SerializeField] private Button ContinueButton;
        [SerializeField] private Canvas CampaignLabels;

        private GameManager gm { get { return GameManager.Instance; }}

        public void Show(){
            HideElement(GameOverLabel);
            HideElement(NextLevelButton);
            HideElement(CampaignLabels);
            HideElement(HighScoreLabel);
            gameObject.SetActive(true);
        }

        public bool IsActive(){
            return gameObject.activeSelf;
        }

        public void Hide(){
            gameObject.SetActive(false);
        }

        public void ShowCampaignLabels() {
            ShowComponent(CampaignLabels);
            SitesDestroyedLabel.SetText("Sites Destroyed: " + gm.CurrentCampaign().SitesDestroyed());
            TanksDestroyedLabel.SetText("Tanks Destroyed: " + gm.CurrentCampaign().TanksDestroyed());
            DeathsLabel.SetText("Deaths: " + gm.CurrentCampaign().NumberOfDeaths());
        }

        public void PlayerWon(){
            ShowGameOverMenu();
            GameOverLabel.SetText("You Won!");
            ShowComponent(GameOverLabel);
            ShowComponent(NextLevelButton);
        }

        public void ShowNewHighScore(){
            ShowComponent(HighScoreLabel);
        }

        public void PlayerLost(){
            ShowGameOverMenu();
            GameOverLabel.SetText("You Lost!");
            ShowComponent(GameOverLabel);
            HideElement(NextLevelButton);
        }

        public void ShowGameOverMenu(){
            Show();
            if(gm.IsCampaignMode) ShowCampaignLabels();
            HideElement(ContinueButton);
        }

        private void ShowComponent(Component obj) {
            obj.gameObject.SetActive(true);
        }

        private void HideElement(Component obj) {
            obj.gameObject.SetActive(false);
        }
    }

}
