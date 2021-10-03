using UnityEngine;

public class SaveGameData
{
    public int HighestScore;
}

public class GameSaver : MonoBehaviour
{
    private const string HighestScoreKey = "HighestScore";

    public SaveGameData CurrentSave { get; private set; }

    private bool IsLoaded => CurrentSave != null;

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(HighestScoreKey, CurrentSave.HighestScore);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }

        CurrentSave = new SaveGameData
        {
            HighestScore = PlayerPrefs.GetInt(HighestScoreKey, 0),
        };
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        CurrentSave = null;
        LoadGame();
    }
}