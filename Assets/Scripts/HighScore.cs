using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScore {

    public int sitesDestroyed;
    public int tanksDestroyed;
    public int numberOfDeaths;

    public HighScore(int sitesDestroyed, int tanksDestroyed, int numberOfDeaths) {
        this.sitesDestroyed = sitesDestroyed;
        this.tanksDestroyed = tanksDestroyed;
        this.numberOfDeaths = numberOfDeaths;
    }

    public int Score(){
        return sitesDestroyed + tanksDestroyed - numberOfDeaths;
    }
}
