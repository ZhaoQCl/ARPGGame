using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace AI.FSM
{
    /// <summary>
    /// 
    /// </summary>
    public enum FSMTriggerID
    {
        //没有生命
        NoHealth,
        //发现目标
        SawTarget,
        //到达目标
        ReachTarget,
        //目标被击杀
        KilledTarget,
        //超出攻击距离
        WithoutAttackRange,
        //丢失目标
        LoseTarget,
        //完成巡逻
        CompletePatrol
    }
}
