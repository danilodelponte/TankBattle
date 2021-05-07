using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public static class HighScoresManager
{
    private const string FILEPATH = "/scores.tbsk";
    private const int maxRecords = 5;

    private static string path { get { return  Application.persistentDataPath + FILEPATH; } }
    private static BinaryFormatter formatter { get { return new BinaryFormatter(); } }

    public static bool SaveScore(int sitesDestroyed, int tanksDestroyed, int numberOfDeaths) {
        HighScore newHighScore = new HighScore(sitesDestroyed, tanksDestroyed, numberOfDeaths);
        HighScore[] scores = LoadScores();
        
        if(scores.Any() && scores.Last().Score() > newHighScore.Score()) return false;
        
        scores = Reorder(scores, newHighScore);
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, scores);
        stream.Close();
        return true;
    }

    public static HighScore[] LoadScores() {
        if(File.Exists(path)) {
            Debug.Log("Save file found in " + path);
            FileStream stream = new FileStream(path, FileMode.Open);
            HighScore[] scoresData = formatter.Deserialize(stream) as HighScore[];
            stream.Close();
            return scoresData;
        } else {
            Debug.Log("Save file not found in " + path);
            return new HighScore[0];
        }
    }

    private static HighScore[] Reorder(HighScore[] scores, HighScore newScore) {
        List<HighScore> scoreList = scores.ToList<HighScore>();
        scoreList.Add(newScore);
        List<HighScore> orderedScores = scoreList.OrderByDescending(score => score.Score()).ToList();
        int exceeedingCount = orderedScores.Count - maxRecords;
        if(exceeedingCount > 0) orderedScores.RemoveRange(maxRecords, exceeedingCount);
        return orderedScores.ToArray();
    }
}
