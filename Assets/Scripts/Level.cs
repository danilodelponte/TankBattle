namespace TankBattle
{

    public enum MapSizeOption
    {
        Small,
        Medium,
        Big,
        Gigantic
    }

    public enum NumberOfEnemiesOption {
        Few,
        More,
        Many_More,
        Overwhelming
    }

    public class LevelState {
        public int TanksDestroyed { get; set; }
        public int SitesDestroyed { get; set; }
        public int Deaths { get; set; }

        public void PlayerLost(){
            TanksDestroyed = 0;
            SitesDestroyed = 0;
            Deaths += 1;
        }
    }

    public class Level
    {
        public MapSizeOption mapSize;
        public NumberOfEnemiesOption numberOfEnemies; 
        public LevelState State { get; }

        public Level(MapSizeOption mapSize = MapSizeOption.Small, NumberOfEnemiesOption numberOfEnemies = NumberOfEnemiesOption.Few) {
            this.mapSize = mapSize;
            this.numberOfEnemies = numberOfEnemies;
            State = new LevelState();
        }

        public int RemainingSites(){
            return NumberOfEnemies() - State.SitesDestroyed;
        }

        public int MapSizeWidth() {
            int mapWidth = 256;
            switch (this.mapSize)
            {
                case MapSizeOption.Gigantic: 
                    mapWidth = 2048; 
                    break;
                case MapSizeOption.Big: 
                    mapWidth = 1024; 
                    break;
                case MapSizeOption.Medium: 
                    mapWidth = 512; 
                    break;
                case MapSizeOption.Small: 
                    mapWidth = 256; 
                    break;
            }
            return mapWidth;
        }

        public int NumberOfEnemies() {
            int nEnemies = 4;

            switch (this.numberOfEnemies)
            {
                case NumberOfEnemiesOption.Overwhelming: nEnemies = 32; break;
                case NumberOfEnemiesOption.Many_More: nEnemies = 16; break;
                case NumberOfEnemiesOption.More: nEnemies = 8; break;
                case NumberOfEnemiesOption.Few: nEnemies = 4; break;
            }
            return nEnemies * ((int)this.mapSize + 1);
        }
    }
}
