using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.Device;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
/*    public GameObject playerPrefab;
    public Transform[] spawnPoints;*/
    public CinemachineTargetGroup targetGroup;
    [SerializeField] UIManager uiManager = UIManager.Instance;
    [SerializeField] UpgradePoolManager upgradePoolManager = UpgradePoolManager.Instance;
    [SerializeField] PlayersManager playersManager = PlayersManager.Instance;
    private int PlayerNumberToSelect = 1;


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
    public void OnPlayersLevelUp() 
    {
        if (AreThereMoreSelections())
        {
            TogglePause();
        }
        uiManager.OnUpgradeSelection(upgradePoolManager.GiveUpgrade(), PlayerNumberToSelect);// fill with upgrades and show level up screen
       

    }
    public void ManageChosenUpgrade(int UpgradeIndex)
    {
        if (PlayerNumberToSelect == 1)
        {
            playersManager.player1Stats.AddUpgrade(upgradePoolManager.UpgradesToSend[UpgradeIndex]);// add the upgrade to the player's stats
        }
        else if (PlayerNumberToSelect == 2)
        {
            playersManager.player2Stats.AddUpgrade(upgradePoolManager.UpgradesToSend[UpgradeIndex]); // add the upgrade to the player's stats
        }
        upgradePoolManager.RemoveUpgradeFromPool(upgradePoolManager.UpgradesToSend[UpgradeIndex]);// remove choosen upgrades from pool



        if (AreThereMoreSelections())
        {
            TogglePlayer();
            OnPlayersLevelUp();
        }
        else
        {
            TogglePlayer();
            uiManager.CloseUpgradeSelection();
            TogglePause();
        }
        
    }

    private void TogglePlayer()
    {
        PlayerNumberToSelect = 3 - PlayerNumberToSelect;
    }

    public bool AreThereMoreSelections() 
    {
        if (PlayerNumberToSelect == 1)  
        {
          return  true; // if the first player
        }
        return false;// if the second player
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
