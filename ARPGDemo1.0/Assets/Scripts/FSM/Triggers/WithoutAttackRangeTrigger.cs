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
    /// ����������������
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            return Vector3.Distance(fsmBase.transform.position, fsmBase.targetTF.position) > fsmBase.chStatus.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }
}
