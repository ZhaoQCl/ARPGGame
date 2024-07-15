using ARPGDemo.MyCharacter;
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
    /// ����Ŀ������
    /// </summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            return fsmBase.targetTF.GetComponent<MyCharacterStatus>().HP <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.KilledTarget;
        }
    }
}
