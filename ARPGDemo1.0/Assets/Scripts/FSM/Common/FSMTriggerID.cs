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
        //û������
        NoHealth,
        //����Ŀ��
        SawTarget,
        //����Ŀ��
        ReachTarget,
        //Ŀ�걻��ɱ
        KilledTarget,
        //������������
        WithoutAttackRange,
        //��ʧĿ��
        LoseTarget,
        //���Ѳ��
        CompletePatrol
    }
}
