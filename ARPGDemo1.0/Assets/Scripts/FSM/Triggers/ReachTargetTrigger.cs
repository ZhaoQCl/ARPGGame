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
    /// 目标进入攻击范围
    /// </summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            //状态机位置 与 目标位置间距
            //判断 小于等于攻击距离
            if (fsmBase.targetTF == null) return false;
            return Vector3.Distance(fsmBase.transform.position, fsmBase.targetTF.position) <= fsmBase.chStatus.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }
}
