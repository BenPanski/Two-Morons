using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedXPManager : MonoBehaviour
{
    public static SharedXPManager Instance { get; private set; }
    public GameManager gameManager;

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }
    public void AddXP(int amount)
    {
        // Add the amount of XP to the player's XP

    }


    public void CheckLevelUp()
    {
        // Check if the player has enough XP to level up
        // If they do, level them up
    }

    public void LevelUp()
    {
        GameManager.Instance.OnPlayerLevelUp();

    }
}
