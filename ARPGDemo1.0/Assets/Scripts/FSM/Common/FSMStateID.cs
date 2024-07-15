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
    /// ״̬���
    /// </summary>
    public enum FSMStateID
    {
        //������״̬
        None,
        //Ĭ��
        Default,
        //����
        Dead,
        //����
        Idle,
        //׷��
        Pursuit,
        //����
        Attacking,
        //Ѳ��
        Patrolling
    }
}
