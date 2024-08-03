using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedXPManager : MonoBehaviour
{
    public static SharedXPManager Instance { get; private set; }
    public GameManager gameManager;
   [SerializeField] List<LevelXPRequirments> LevelRequirments = new List<LevelXPRequirments>();
    public int CurrentLevel = 1;
    public int CurrentXP = 0;

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
        print("XP before addition: " + CurrentXP);
        CurrentXP += amount;
        // Add the amount of XP to the player's XP
        CheckLevelUp();
    }


    public void CheckLevelUp()
    {
        print("Current XP: " + CurrentXP);
        if (CurrentXP >= LevelRequirments[CurrentLevel].xpRequired) // Check if the player has enough XP to level up
        {
            
            print("Level up");
            LevelUp(); // If they do, level them up
        }
    }

    public void LevelUp()
    {
        CurrentLevel++;
        GameManager.Instance.OnPlayersLevelUp();

    }
}
[System.Serializable]
public class LevelXPRequirments
{
    public int levelIndex;
    public int xpRequired;
}