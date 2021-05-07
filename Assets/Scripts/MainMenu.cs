using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace TankBattle
{       
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown customMapSizeDropdown;
        [SerializeField] private TMP_Dropdown customEnemiesDropdown;
        private GameManager gameManager;

        private void Awake() {
            gameManager = GameManager.Instance;
        }

        public void StartGame(){
            gameManager.SelectedGameMode = GameMode.Campaign;
            gameManager.LoadGameplay();
        }

        public void StartCustomGame(){
            gameManager.SelectedGameMode = GameMode.Custom;
            var customMapSize = (MapSizeOption) customMapSizeDropdown.value;
            var customNumberOfEnemies = (NumberOfEnemiesOption) customEnemiesDropdown.value;
            Level customLevel = new Level(customMapSize, customNumberOfEnemies);
            gameManager.SetCustomLevel(customLevel);
            gameManager.LoadGameplay();
        }

        public void ExitGame(){
            Debug.Log("Exit game!");
            Application.Quit();
        }
    }
}
