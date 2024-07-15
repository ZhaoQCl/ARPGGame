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
    /// 状态类
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID StateID {  get; set; }

        //映射表
        private Dictionary<FSMTriggerID, FSMStateID> map;
        //条件列表
        private List<FSMTrigger> triggers;

        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggers = new List<FSMTrigger>();
            Init();
        }
        public abstract void Init();

        //由状态机调用（为映射表和条件列表赋值）
        public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
        {
            //添加映射
            map.Add(triggerID, stateID);
            //创建条件对象
            CreateTrigger(triggerID);
        }

        private void CreateTrigger(FSMTriggerID triggerID)
        {
            //创建条件对象
            //命名规范：AI.FSM.+条件枚举+Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(trigger);
        }

        //为具体状态类提供可选实现
        public virtual void EnterState(FSMBase fsmBase) { }
        public virtual void ActionState(FSMBase fsmBase) { }
        public virtual void ExitState(FSMBase fsmBase) { }

        //检测当前状态的条件是否满足
        public void Reason(FSMBase fsmBase)
        {
            for(int i = 0; i < triggers.Count; i++)
            {
                //条件满足
                if (triggers[i].HandleTrigger(fsmBase))
                {
                    //从映射表中获取输出状态
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    //切换状态
                    fsmBase.ChangeActiveState(stateID);
                    return;
                }
            }
        }
    }
}
