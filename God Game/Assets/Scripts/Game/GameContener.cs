using UnityEngine;
using System.Collections;

/// <summary>
/// Can be used only outside start/awake
/// </summary>
public static class GameContener
{
    public static GameObject Player1;
    public static GameObject Player2;
    public static GameObject[] Players = new GameObject[2];
    public static GameObject God;
    public static void FreezePlayers()
    {
        foreach (var player in Players)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.GetComponent<PlayerController>().enabled = false;
        }

        God.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        God.GetComponent<GodController>().enabled = false;
    }

    public static void UnfreezePlayers()
    {
        foreach (var player in Players)
        {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            player.GetComponent<PlayerController>().enabled = true;
        }

        God.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        God.GetComponent<GodController>().enabled = true;
    }
    
    public static void MovePlayersToPosition(Vector3 position)
    {
        short i = 0;
        foreach (var player in Players)
        {
            player.transform.position = new Vector3(position.x + i,position.y,position.z);
            player.gameObject.SetActive(true);
            i += 10;
        }
        God.transform.position = new Vector3(position.x + 5, position.y + 4, position.z);
        God.gameObject.SetActive(true);
    }
	public static void Initialize ()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        Players[0] = Player1;
        Players[1] = Player2;

        God = GameObject.Find("God");
	}
}
