/*  _    _
   (o)--(o)
  /.______.\
  \________/    代码神宠
 ./        \.
( .        , )
 \ \_\\//_/ /
  ~~  ~~  ~~
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_FSMMachine : BaseMachine
{
    // 实体
    public HeroBehaviour heroBehaviour;
    // 当前执行的状态
    BaseState curState;
    // 状态集合
    Dictionary<PlayerState, BaseState> AllState_Dic;
    // 上个状态枚举
    PlayerState lateState;
    // 当前状态枚举
    private PlayerState _state;
    public PlayerState state
    {
        get => _state;
        set
        {
            _state = value;
        }
    }

    public Hero_FSMMachine(HeroBehaviour heroBehaviour)
    {
        this.heroBehaviour = heroBehaviour;
        OnInit();
    }


    /// <summary>
    /// 初始化状态机
    /// </summary>
    public override void OnInit()
    {
        AllState_Dic = new Dictionary<PlayerState, BaseState>();
        AllState_Dic.Add(PlayerState.IDLE,new PlayerFSM_IDLE(heroBehaviour));
        AllState_Dic.Add(PlayerState.RUN, new PlayerFSM_RUN(heroBehaviour));
    }


    /// <summary>
    /// 设置状态
    /// </summary>
    public override void SetState(int _state)
    {
        SetState_logic((PlayerState)_state);
    }

    public void SetState(PlayerState _state)
    {
        SetState_logic(_state);
    }

    private void SetState_logic(PlayerState _state)
    {
        if (state == _state)
            return;

        if (IsCanChangeState((_state)))
        {
            ChangeState(_state);
        }
        else
        {
            Debug.LogWarning("状态切换失败 请重试!");
        }
    }




    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="playerState"></param>
    private void ChangeState(PlayerState playerState)
    {
        if(AllState_Dic.ContainsKey(playerState))
        {
            curState?.OnExit();
            curState = AllState_Dic[playerState];
            state = playerState;
            curState.OnEnter();
        }
        
    }


    /// <summary>
    /// 是否可切换到此状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool IsCanChangeState(PlayerState playerState)
    {
        return true;
    }


    /// <summary>
    /// 状态更新
    /// </summary>
    public override void OnUpdate()
    {
        if (curState != null)
        {
            curState.OnUpdate();
        }
    }



}
