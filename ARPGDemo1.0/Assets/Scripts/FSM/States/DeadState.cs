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
    public class DeadState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Dead;
        }
        public override void EnterState(FSMBase fsmBase)
        {
            base.EnterState(fsmBase);
            //½ûÓÃ×´Ì¬»ú
            fsmBase.enabled = false;
        }
    }
}
