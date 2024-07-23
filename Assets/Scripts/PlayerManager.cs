using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Transform player1;
    public Transform player2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Transform GetOtherPlayer(Transform currentPlayer)
    {
        if (currentPlayer == player1)
        {
            return player2;
        }
        else if (currentPlayer == player2)
        {
            return player1;
        }
        return null;
    }
    public Transform GetClosestPlayerTransform(Transform yourTransform)
    {
        if (player1 == null && player2 == null)
        {
            return null;
        }
        else if (player1 == null)
        {
            return player2;
        }
        else if (player2 == null)
        {
            return player1;
        }

        float distanceToPlayer1 = Vector3.Distance(yourTransform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(yourTransform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }
    public int  GetClosestPlayerNumber(Transform yourTransform)
    {
        if (player1 == null && player2 == null)
        {
            Debug.LogError("No players found");
            return 3;
        }
        else if (player1 == null)
        {
            Debug.LogError("No player 1");
            return 2;
        }
        else if (player2 == null)
        {
            Debug.LogError("No player 2");
            return 1;
        }

        float distanceToPlayer1 = Vector3.Distance(yourTransform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(yourTransform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    public Vector3 GetPlayerPosition(int playerIndex)
    {
        if (playerIndex == 1 && player1 != null)
        {
            return player1.position;
        }
        else if (playerIndex == 2 && player2 != null)
        {
            return player2.position;
        }
        return Vector3.negativeInfinity;
    }

    
}
