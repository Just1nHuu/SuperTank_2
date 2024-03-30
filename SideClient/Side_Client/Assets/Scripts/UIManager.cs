using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;

    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instace already exists, destroying duplicate");
                Destroy(value);
            }
        }
    }


    [Header("Connect")]
    [SerializeField] private GameObject connectUI;
    [SerializeField] private InputField usenameField;
   

    private void Awake()
    {
        Singleton = this;
    }

    public void ConnectClicked()
    {
        usenameField.interactable = false;
        connectUI.SetActive(false);

        NetworkManager.Singleton.Connect();
    }

    public void BackToMain()
    {
        usenameField.interactable = true;
        connectUI.SetActive(true);
    }

    public void SendName()
    {
        Message message = Message.Create(MessageSendMode.Reliable,(ushort)ClientToServerId.name);
        message.AddString(usenameField.text);
        NetworkManager.Singleton.Client.Send(message);
    }
}
