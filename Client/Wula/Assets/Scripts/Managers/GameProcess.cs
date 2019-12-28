using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏流程
/// </summary>
public class GameProcess : MonoBehaviour
{
    public NetworkManager NetworkMgr;
    public ClientManager ClientManager;
    public MessageManager MessageMgr;
    public bool beginUpdate = false;

    private void Awake()
    {
        StartCoroutine(RunTheGame()); 
    }

    IEnumerator RunTheGame()
    {
        yield return ClientManager.StartCoroutine(ClientManager.OnAwake());

        yield return NetworkMgr.StartCoroutine(NetworkMgr.OnAwake());

        yield return MessageMgr.StartCoroutine(MessageMgr.OnAwake());

        beginUpdate = true;
        yield return null;
        SceneManager.LoadSceneAsync("Main");
    }

    private void Update()
    {
        if (!beginUpdate)
            return;

        NetworkMgr.OnUpdate();
        ClientManager.OnUpdate();
        MessageMgr.OnUpdate();
    }

    void InitManagers()
    {

    }
}

