using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HeroSkills
{
    Comb_1,
    Comb_2,
    Comb_3,
    Comb_4,
    Comb_5,
    Comb_6,
    Skill_1,
    Skill_2,
    Skill_3,
    Special_1
}


/// <summary>
/// 英雄战斗组件
/// </summary>
public class HeroFightment : BaseFightment
{
    HeroBehaviour heroBehaviour;

    public Dictionary<>

    public override void OnInit(BaseCharacter character)
    {
        base.OnInit(character);
        heroBehaviour = character as HeroBehaviour;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }


    public virtual void CastSkill()
    {

    }
}
