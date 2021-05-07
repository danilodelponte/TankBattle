using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] Terrain terrain;
    [SerializeField] public int depth = 10;
    [SerializeField] public int width = 256;
    [SerializeField] public int height = 256;

    [SerializeField] public float scale = 5f;
    [SerializeField] public float offsetX = 0f;
    [SerializeField] public float offsetY = 0f;

    [SerializeField] private AnimationCurve heightCurve;

    public Terrain GenerateTerrain() {
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
        return terrain;
    }

    public TerrainData GenerateTerrainData(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0,0,GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights(){
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                float height = CalculateHeight(x,y);
                heights[x,y] = heightCurve.Evaluate(height);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y) {
        float xCoord = (float) x / scale + offsetX;
        float yCoord = (float) y / scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
