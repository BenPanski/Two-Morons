using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;
    public CinemachineTargetGroup targetGroup;

    private void Start()
    {
       // SpawnPlayers();
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
}
