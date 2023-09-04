using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    private const string SAVE_KEY = "SAVE";
    private SaveData saveData;

    public void Save()
    {
        var saveString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, saveString);
    }

    public void LoadSave()
    {
        var saveString = PlayerPrefs.GetString(SAVE_KEY);
        if (string.IsNullOrWhiteSpace(saveString))
        {
            saveData = new SaveData();
        }
        else
        {
            saveData = JsonUtility.FromJson<SaveData>(saveString);
        }
    }

    public List<uint> GetHighscore()
    {
        return saveData.Highscore;
    }

    public uint GetHighestScore()
    {
        var highscore = GetHighscore();
        return highscore.Count > 0
            ? highscore.First()
            : 0;
    }

    public void AddScoreToHighscores(uint score)
    {
        if (saveData.Highscore.Count.Equals(GameSettingsManager.Instance.Settings.MaxHighscoresSaveCount))
        {
            if(saveData.Highscore.Last() >= score) return;

            saveData.Highscore.Remove(saveData.Highscore.Last());
        }

        AddScoreToSaveData(score);
    }

    private void AddScoreToSaveData(uint score)
    {
        saveData.Highscore.Add(score);
        saveData.Highscore.Sort((x, y) => y.CompareTo(x));
    }
}
