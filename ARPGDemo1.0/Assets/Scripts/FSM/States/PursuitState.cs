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
    /// ׷��״̬
    /// </summary>
    public class PursuitState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }
        public override void EnterState(FSMBase fsmBase)
        {
            base.EnterState(fsmBase);
            //�����ƶ�����
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.run, true);
        }
        public override void ActionState(FSMBase fsmBase)
        {
            base.ActionState(fsmBase);
            fsmBase.MoveToTarget(fsmBase.targetTF.position, fsmBase.chStatus.attackDistance, fsmBase.runSpeed);
        }
        public override void ExitState(FSMBase fsmBase)
        {
            base.EnterState(fsmBase);
            //ֹͣ�ƶ�
            fsmBase.StopMove();
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.run, false);
        }
    }
}
