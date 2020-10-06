using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float highScore;
    public string level;

    public PlayerData(GameManager gameManager)
    {
        highScore = gameManager.Score();
        level = gameManager.CurrentLevel();
    }
}
