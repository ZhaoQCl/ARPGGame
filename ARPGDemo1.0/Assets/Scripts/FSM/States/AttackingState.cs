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
    /// ¹¥»÷×´Ì¬
    /// </summary>
    public class AttackingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Attacking;
        }
        private float atkTime;
        public override void ActionState(FSMBase fsmBase)
        {
            base.ActionState(fsmBase);
            if (atkTime <= Time.time)
            {
                fsmBase.skillSystem.UseRandomSkill();
                atkTime = Time.time + fsmBase.chStatus.attackInterval;
            }
            
        }
    }
}
