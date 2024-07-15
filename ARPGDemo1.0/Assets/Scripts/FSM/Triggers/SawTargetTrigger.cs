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
    /// 看见目标条件
    /// </summary>
    public class SawTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            return fsmBase.targetTF != null;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.SawTarget;
        }
    }
}
