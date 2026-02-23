using System;
using UnityEditor.Overlays;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ISaveService _saveService;
    private int currentLevel;
    private int currentScore;
    private float playerHeath;
    
    public void Initialize(ISaveService saveService)
    {
        _saveService = saveService;
    }

    public void SaveGame()
    {
        var saveData = new SaveData()
        {
            level = currentLevel,
            score = currentScore,
            health = playerHeath
        };
        _saveService.Save("game_save", saveData);
    }

    public void LoadGame()
    {
        var saveData = _saveService.Load<SaveData>("game_save");
        currentLevel = saveData.level;
        currentScore = saveData.score;
        playerHeath = saveData.health;
    }
}

[Serializable]
public class SaveData
{
    public int level;
    public int score;
    public float health;
}