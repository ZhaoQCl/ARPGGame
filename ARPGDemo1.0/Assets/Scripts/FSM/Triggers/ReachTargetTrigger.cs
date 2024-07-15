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
    /// Ŀ����빥����Χ
    /// </summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsmBase)
        {
            //״̬��λ�� �� Ŀ��λ�ü��
            //�ж� С�ڵ��ڹ�������
            if (fsmBase.targetTF == null) return false;
            return Vector3.Distance(fsmBase.transform.position, fsmBase.targetTF.position) <= fsmBase.chStatus.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }
}
