using UnityEngine.SceneManagement;
using UnityEngine;

namespace TankBattle
{
    public enum GameMode
    {
        Campaign,
        Custom
    }

    static class Scenes
    {
        public const string Gameplay = "Gameplay";
        public const string MainMenu = "MainMenu";
    }

    public class GameManager : Singleton<GameManager>
    {
        public GameMode SelectedGameMode { get; set; } = GameMode.Campaign;
        public bool IsCampaignMode { get { return SelectedGameMode == GameMode.Campaign; }}
        private Campaign currentCampaign;
        private Level customLevel;

        private void Awake() {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
            if(scene.name == Scenes.MainMenu) {
                currentCampaign = null;
                customLevel = null;
            }
        }

        public Campaign CurrentCampaign(){
            if(currentCampaign == null) {
                currentCampaign = new Campaign();
            }

            return currentCampaign;
        }

        public void LoadGameplay() {
            SceneManager.LoadSceneAsync(Scenes.Gameplay);
        }
        
        public void LoadMainMenu() {
            SceneManager.LoadSceneAsync(Scenes.MainMenu);
        }

        public void CampaignNextLevel(){
            currentCampaign.NextLevel();
            LoadGameplay();
        }

        public Level CurrentLevel(){
            if(SelectedGameMode == GameMode.Campaign) {
                return CurrentCampaign().CurrentLevel();
            } else {
                if(customLevel == null) customLevel = new Level();
            }
            return customLevel;
        }

        public void SetCustomLevel(Level customLevel) {
            this.customLevel = customLevel;
        }
    }
}