using System.Collections;
using System.Collections.Generic;
using Riptide;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

    public ushort Id { get; private set; }

    public string Username { get; private set; }

    [MessageHandler((ushort)ClientToServerId.name)]

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn (ushort id, string username)
    {
        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity).GetComponent<Player>();
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)}";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        list.Add(id, player);
    }

    private static void Name(ushort fromCientId, Message message)
    {
        Spawn(fromCientId, message.GetString());
    }
}
