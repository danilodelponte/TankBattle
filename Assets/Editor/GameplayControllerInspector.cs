using System;
using UnityEngine;
using UnityEditor;

namespace TankBattle
{
    [CustomEditor (typeof(GameplayController))]
    public class GameplayControllerInspector : Editor
    {
        private int selectedMapSize = 0;
        private int selectedNumberOfEnemies = 0;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            MapSizeOption mapSize = MapSizeDropdown();
            NumberOfEnemiesOption nEnemies = NumberOfEnemiesDropdown();
            if(GUILayout.Button("Generate Terrain")){
                GameplayController controller = (GameplayController) target;
                controller.GenerateGame(new Level(mapSize, nEnemies));
            };
        }

        private MapSizeOption MapSizeDropdown() {
            string[] options = Enum.GetNames(typeof(MapSizeOption));
            selectedMapSize = EditorGUILayout.Popup("Map Size", selectedMapSize, options); 
            return (MapSizeOption) selectedMapSize;
        }

        private NumberOfEnemiesOption NumberOfEnemiesDropdown() {
            string[] options = Enum.GetNames(typeof(NumberOfEnemiesOption));
            selectedNumberOfEnemies = EditorGUILayout.Popup("Number of Enemies", selectedNumberOfEnemies, options); 
            return (NumberOfEnemiesOption) selectedNumberOfEnemies;
        }
    }
}