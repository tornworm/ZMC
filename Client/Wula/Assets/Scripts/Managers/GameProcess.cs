using System.Collections;
using System.Collections.Generic;
using System.Text;
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
    public UIManager UIMgr;
    public ResourcesManager resourcesMgr;
    public GameSceneManager gameSceneMgr;
    bool beginUpdate = false;

    private void Awake()
    {
        StartCoroutine(RunTheGame()); 
    }

    IEnumerator RunTheGame()
    {
        yield return ClientManager.StartCoroutine(ClientManager.OnAwake());

        yield return NetworkMgr.StartCoroutine(NetworkMgr.OnAwake());

        yield return MessageMgr.StartCoroutine(MessageMgr.OnAwake());

        yield return UIMgr.StartCoroutine(UIMgr.OnAwake());

        yield return resourcesMgr.StartCoroutine(resourcesMgr.OnAwake());

        yield return gameSceneMgr.StartCoroutine(gameSceneMgr.OnAwake());

        beginUpdate = true;
        gameSceneMgr.LoadSceneShowLoadingAsync("Login", () => UIManager.Instance.ShowPanel(UIPanelType.PanelLogin, PanelFrom.Normal));
    }

    private void Update()
    {
        if (!beginUpdate)
            return;

        NetworkMgr.OnUpdate();
        ClientManager.OnUpdate();
        MessageMgr.OnUpdate();
        UIMgr.OnUpdate();
        resourcesMgr.OnUpdate();
        gameSceneMgr.OnUpdate();
    }

    void InitManagers()
    {

    }
    private void OnApplicationQuit()
    {

        Request request = new Request(Protocol_C2S.OnClientExit, "f");
        // NetworkManager.Instance.Send(request);
        StringBuilder stringBuilder = new StringBuilder();
        //消息处理
        stringBuilder
        .Append(request.requestID.ToString())
        .Append("&")
        .Append(request.data);

        ClientManager.Instance.Send(stringBuilder.ToString());
        stringBuilder.Length = 0;
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
    }
   
}

