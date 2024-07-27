using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager Instance { get; private set; }
    [SerializeField] Color Player1Color;
    [SerializeField] Color Player2Color;

    public Transform player1;
    public Transform player2;
    public PlayerStats player1Stats;
    public PlayerStats player2Stats;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (player1Stats == null)
        {
            player1Stats = player1.gameObject.GetComponent<PlayerStats>();
        }
        if (player2Stats == null)
        {
            player2Stats = player2.gameObject.GetComponent<PlayerStats>();
        }
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

    public Color GetPlayerColor(int playerIndex) 
    {
        if (playerIndex == 1 && player1 != null)
        {
            return Player1Color;
        }
        else if (playerIndex == 2 && player2 != null)
        {
            return Player2Color;
        }
        return Color.white;
    }
}
