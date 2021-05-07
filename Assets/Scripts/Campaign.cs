using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TankBattle {
    
    public class Campaign {
        private Level[] levels;
        private int currentLevelIdx;

        public Campaign() {
            Levels();
            currentLevelIdx = 0;
        }

        private Level[] Levels() {
            if(levels == null) 
            {
                levels = new Level[] {
                    new Level(MapSizeOption.Small, NumberOfEnemiesOption.Few),
                    new Level(MapSizeOption.Small, NumberOfEnemiesOption.More),
                    new Level(MapSizeOption.Medium, NumberOfEnemiesOption.Few),
                    new Level(MapSizeOption.Medium, NumberOfEnemiesOption.More),
                    new Level(MapSizeOption.Big, NumberOfEnemiesOption.More),
                    new Level(MapSizeOption.Small, NumberOfEnemiesOption.Many_More),
                    new Level(MapSizeOption.Medium, NumberOfEnemiesOption.Many_More),
                    new Level(MapSizeOption.Big, NumberOfEnemiesOption.Many_More),
                    new Level(MapSizeOption.Gigantic, NumberOfEnemiesOption.More),
                    new Level(MapSizeOption.Small, NumberOfEnemiesOption.Overwhelming),
                    new Level(MapSizeOption.Medium, NumberOfEnemiesOption.Overwhelming),
                    new Level(MapSizeOption.Big, NumberOfEnemiesOption.Overwhelming)
                };
            }
            return levels;
        }

        public Level CurrentLevel(){
            return levels[currentLevelIdx];
        }

        public Level NextLevel(){
            currentLevelIdx += 1;
            return levels[currentLevelIdx];
        }

        public Level Level(int levelNumber){
            return levels[levelNumber];
        }

        public int TanksDestroyed(){
            return levels.Aggregate(0, (result, level) => result + level.State.TanksDestroyed);
        }

        public int SitesDestroyed(){
            return levels.Aggregate(0, (result, level) => result + level.State.SitesDestroyed);
        }

        public int NumberOfDeaths(){
            return levels.Aggregate(0, (result, level) => result + level.State.Deaths);
        }
    }
}