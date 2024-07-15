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
    /// ��ʧĿ������
    /// </summary>
    public class LoseTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            return fsmBase.targetTF == null;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.LoseTarget;
        }
    }
}
