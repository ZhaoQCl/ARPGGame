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
    /// ״̬��
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID StateID {  get; set; }

        //ӳ���
        private Dictionary<FSMTriggerID, FSMStateID> map;
        //�����б�
        private List<FSMTrigger> triggers;

        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggers = new List<FSMTrigger>();
            Init();
        }
        public abstract void Init();

        //��״̬�����ã�Ϊӳ���������б�ֵ��
        public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
        {
            //���ӳ��
            map.Add(triggerID, stateID);
            //������������
            CreateTrigger(triggerID);
        }

        private void CreateTrigger(FSMTriggerID triggerID)
        {
            //������������
            //�����淶��AI.FSM.+����ö��+Trigger
            Type type = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(trigger);
        }

        //Ϊ����״̬���ṩ��ѡʵ��
        public virtual void EnterState(FSMBase fsmBase) { }
        public virtual void ActionState(FSMBase fsmBase) { }
        public virtual void ExitState(FSMBase fsmBase) { }

        //��⵱ǰ״̬�������Ƿ�����
        public void Reason(FSMBase fsmBase)
        {
            for(int i = 0; i < triggers.Count; i++)
            {
                //��������
                if (triggers[i].HandleTrigger(fsmBase))
                {
                    //��ӳ����л�ȡ���״̬
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    //�л�״̬
                    fsmBase.ChangeActiveState(stateID);
                    return;
                }
            }
        }
    }
}
