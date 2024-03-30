using Riptide.Utils;
using Riptide;
using UnityEngine;
using System.Runtime.CompilerServices;

public enum ClientToServerId : ushort
{
    name = 1,
}

public class NetworkManager : MonoBehaviour
{
    // điều này giúp gắn trình quản lý mạng vào một đối tượng trò chơi và truy cập phiên bản cụ thể đó từ bất kỳ đâu trong mã 
    //nó cũng sẽ đảm bảo rằng sẽ chỉ có trở thành một phiên bản của trình quản lý mạng
    private static NetworkManager _singleton;
    
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instace already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }
    public Server Server { get; private set; }

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;



    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
    }

    private void FixedUpdate()
    {
        Server.Update();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    private void PlayerLeft(object sender, ClientConnectedEventArgs e)
    {
        Destroy(Player.list[e.Id].gameObject);
    }
}

