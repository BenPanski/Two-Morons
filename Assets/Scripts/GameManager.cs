using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public CinemachineTargetGroup targetGroup;
    public int Playerlevel;
    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    /* private void SpawnPlayers()
     {
         for (int i = 0; i < spawnPoints.Length; i++)
         {
             // Instantiate player at the designated spawn point
             var player = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);

             // Get PlayerInput component
             var playerInput = player.GetComponent<PlayerInput>();

             // Set the correct action map based on player index
             string actionMap = i == 0 ? "Player1" : "Player2";
             playerInput.defaultActionMap = actionMap;
             playerInput.SwitchCurrentActionMap(actionMap);

             // Add player to the CinemachineTargetGroup
             targetGroup.AddMember(player.transform, 1f, 2f);
         }
     }*/
    public void OnPlayerLevelUp() 
    {
        Playerlevel++;
      // UIManager.Instance.OnUpgradeSelection();
        TogglePause();
        // show level up screen
        // Increase the player's level by 1
        // Increase the player's stats
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
