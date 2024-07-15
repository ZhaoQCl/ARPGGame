using System;
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
    /// Ñ²Âß×´Ì¬
    /// </summary>
    public class PatrollingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }

        public override void EnterState(FSMBase fsmBase)
        {
            base.EnterState(fsmBase);
            fsmBase.isPatrolComplete = false;
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.walk, true);
        }

        public override void ActionState(FSMBase fsmBase)
        {
            base.ActionState(fsmBase);
            switch (fsmBase.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsmBase);
                    break;
                case PatrolMode.Loop:
                    LoopPatrolling(fsmBase);
                    break;
                case PatrolMode.PingPong:
                    PingPongPatrolling(fsmBase);
                    break;
            }
        }

        private int index;

        private void PingPongPatrolling(FSMBase fsmBase)
        {
            if (Vector3.Distance(fsmBase.transform.position, fsmBase.wayPoints[index].position) < 0.5f)
            {
                if (index == fsmBase.wayPoints.Length - 1)
                {
                    Array.Reverse(fsmBase.wayPoints);
                    index++;
                }
                index = (index + 1) % fsmBase.wayPoints.Length;
            }
            fsmBase.MoveToTarget(fsmBase.wayPoints[index].position, 0, fsmBase.walkSpeed);
        }

        private void LoopPatrolling(FSMBase fsmBase)
        {
            if (Vector3.Distance(fsmBase.transform.position, fsmBase.wayPoints[index].position) < 0.5f)
            {
                index = (index + 1) % fsmBase.wayPoints.Length;
            }
            fsmBase.MoveToTarget(fsmBase.wayPoints[index].position, 0, fsmBase.walkSpeed);
        }

        private void OncePatrolling(FSMBase fsmBase)
        {
            if (Vector3.Distance(fsmBase.transform.position, fsmBase.wayPoints[index].position) < 0.5f)
            {
                if (index == fsmBase.wayPoints.Length - 1)
                {
                    fsmBase.isPatrolComplete = true;
                    return;
                }
                index++;
            }
            fsmBase.MoveToTarget(fsmBase.wayPoints[index].position,0,fsmBase.walkSpeed);
        }

        public override void ExitState(FSMBase fsmBase)
        {
            base.ExitState(fsmBase);
            fsmBase.anim.SetBool(fsmBase.chStatus.chParams.walk, false);
        }
    }
}
