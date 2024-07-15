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
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fsmBase)
        {
            base.EnterState(fsmBase);
            //²¥·Å´ý»ú¶¯»­
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.idle, true);
        }

        public override void ExitState(FSMBase fsmBase)
        {
            base.ExitState(fsmBase);
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.idle, false);
        }

    }
}
