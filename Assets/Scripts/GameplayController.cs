using UnityEngine;
using TMPro;

namespace TankBattle {

    public enum PlayerStates {
        PLAYING,
        WON,
        LOST
    }
        
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerTank;
        [SerializeField] private GameObject enemyTanksParent;
        [SerializeField] private GameObject EnemyTankPrefab;
        [SerializeField] private int nTanksPerSite;
        [SerializeField] private float tanksRangeFromSite = 10f;
        [SerializeField] private GameObject enemySitesParent;
        [SerializeField] private GameObject EnemySitePrefab;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private InGameUI inGameLabels;
        private TerrainData terrainData;
        private PlayerStates PlayerState; 

        public static GameplayController Singleton;
        private GameManager gm { get { return GameManager.Instance; }}
        private Campaign campaign;
        private Level level;

        void Awake() {
            if (Singleton != null) {
                Destroy(gameObject);
                return;
            }
            Singleton = this;

            if(gm.IsCampaignMode) campaign = gm.CurrentCampaign();
            level = gm.CurrentLevel();
            PlayerState = PlayerStates.PLAYING;
            GenerateGame(level);
        }

        public void GenerateGame(Level level){
            GenerateTerrain(level.MapSizeWidth());
            PlaceEnemies(level.NumberOfEnemies());
            PlacePlayer();
        }

        public void GenerateTerrain(int mapSize){
            TerrainGenerator generator = GetComponent<TerrainGenerator>();
            generator.offsetX = Random.Range(0,9999);
            generator.offsetY = Random.Range(0,9999);
            generator.width = mapSize;
            generator.height = mapSize;
            
            this.terrainData = generator.GenerateTerrain().terrainData;
        }

        public void PlacePlayer() {
            Vector3 playerPosition = PlayerTank.transform.position;
            playerPosition.y = terrainData.GetHeight(30, 30);
            PlayerTank.transform.position = playerPosition;
        }

        public void ClearEnemies(){
            while(enemyTanksParent.transform.childCount > 0){
                DestroyImmediate(enemyTanksParent.transform.GetChild(0).gameObject);
            }

            while(enemySitesParent.transform.childCount > 0){
                DestroyImmediate(enemySitesParent.transform.GetChild(0).gameObject);
            }
        }

        public void PlaceEnemies(int nEnemies){
            ClearEnemies();
            int nAreas = 144;
            int nDivisions = (int) Mathf.Sqrt(nAreas);
            Vector3 mapSize = terrainData.size;
            float areaWidth = mapSize.x / nDivisions;
            float areaHeight = mapSize.z / nDivisions;
            bool[,] areas = new bool[nDivisions,nDivisions];
            for (int i = 0; i < nEnemies; i++)
            {
                int x = Random.Range(1, nDivisions);
                int y = Random.Range(1, nDivisions);
                if (areas[x,y])
                {
                    for(int j = 1; j < nDivisions; j++)
                    {
                        x = j;
                        for(int k = 1; k < nDivisions; k++) {
                            y = k;
                            if(!areas[x,y]) break;
                        }
                        if(!areas[x,y]) break;
                    }
                }
                areas[x,y] = true;

                int posX = Mathf.FloorToInt(x * areaWidth + Random.Range(1, areaWidth - 1));
                int posZ = Mathf.FloorToInt(y * areaHeight + Random.Range(1, areaHeight - 1));
                float posY = terrainData.GetHeight(posX, posZ) + .5f;
                Vector3 sitePosition = new Vector3(posX, posY, posZ);
                Quaternion randomRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                GameObject enemySite = GameObject.Instantiate(EnemySitePrefab, sitePosition, randomRotation);
                enemySite.transform.SetParent(enemySitesParent.transform);

                posX = Mathf.FloorToInt(posX + tanksRangeFromSite * Random.Range(-1,1));
                posZ = Mathf.FloorToInt(posZ + tanksRangeFromSite * Random.Range(-1,1));
                posY = terrainData.GetHeight(posX, posZ);
                sitePosition = new Vector3(posX, posY, posZ);
                randomRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
                GameObject enemyTank = GameObject.Instantiate(EnemyTankPrefab, sitePosition, randomRotation);
                enemyTank.transform.SetParent(enemyTanksParent.transform);
            }
        }

        private void Start() {
            UpdateState();
        }

        private void Update() {
            if(PlayerState == PlayerStates.PLAYING) {
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    if(pauseMenu.IsActive()) ResumeGame();
                    else PauseGame();
                }
            }
        }

        public void PauseGame ()
        {
            inGameLabels.Hide();
            pauseMenu.Show();
            Time.timeScale = 0;
        }

        public void ResumeGame ()
        {
            pauseMenu.Hide();
            inGameLabels.Show();
            Time.timeScale = 1;
        }

        public void RestartScene ()
        {
            gm.LoadGameplay();
            Time.timeScale = 1;
        }

        public void ExitToMainMenu(){
            gm.LoadMainMenu();
            Time.timeScale = 1;
        }

        public void GoToNextLevel(){
            gm.CampaignNextLevel();
        }
        
        public void PlayerHit() {
            PlayerLost();
        }

        public void SiteHit(GameObject Site) {
            level.State.SitesDestroyed += 1;
            UpdateState();
        }

        public void TankHit(GameObject Tank) {
            level.State.TanksDestroyed += 1;
            UpdateState();
        }

        public void UpdateState() {
            if(level.RemainingSites() == 0) PlayerWon();
            inGameLabels.UpdateLabels();
        }

        public void PlayerWon() {
            PlayerState = PlayerStates.WON;
            pauseMenu.PlayerWon();
            bool highScore = HighScoresManager.SaveScore(
                level.State.SitesDestroyed, 
                level.State.TanksDestroyed, 
                level.State.Deaths 
            );
            if(highScore) pauseMenu.ShowNewHighScore();
        }

        public void PlayerLost() {
            PlayerState = PlayerStates.LOST;
            level.State.PlayerLost();
            pauseMenu.PlayerLost();
        }
    }
}
