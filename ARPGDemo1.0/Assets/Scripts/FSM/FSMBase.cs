using ARPGDemo.MyCharacter;
using ARPGDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

/*
    ��Ҫ���������̣�
    ״̬��ÿ֡��⵱ǰ״̬������ --> ״̬��������е��������� --> 
    ���ĳ��������� --> ״̬���л�״̬

    ��ϸ���̣�
    Star
 */
namespace AI.FSM
{
    /// <summary>
    /// ״̬��
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        /*
        ��Ҫ���������̣�
        ״̬��ÿ֡��⵱ǰ״̬������ --> ״̬��������е��������� --> 
        ���ĳ��������� --> ״̬���л�״̬

        ��ϸ���̣�
        Start
        {
            ��ʼ���������
            ����״̬��
            {
                ����״̬�б�
                ����״̬����
                ���ӳ�� --> ��ӵ�״̬�б�
            }
            ��ʼ��Ĭ��״̬
            {
                ���ݱ༭��Ĭ��״̬ID���õ�ǰ״̬������
            }
        }
        Update
        {
            ��⵱ǰ״̬����
            {
                ���������б����ÿ�������Ƿ����㣻
                �������������ɣ���ͨ��״̬�������л������õ�ǰ״̬����
            }
            ִ�е�ǰ״̬��Ϊ
        }
        */
        #region �ű���������
        public void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
        }


        //ÿ֡������߼�
        public void Update()
        {
            //�ж������Ƿ�����.....
            currentState.Reason(this);
            //ִ�е�ǰ״̬�߼�.....
            currentState.ActionState(this);
            //����Ŀ��
            SearchTarget();
        }
        #endregion

        #region ״̬�������Ա

        //״̬�б�
        private List<FSMState> states;

        [Tooltip("Ĭ��״̬���")]
        public FSMStateID defaultStateID;

        //��ǰ״̬
        private FSMState currentState;
        //��ʼ��Ĭ��״̬
        private FSMState defaultState;
        [Tooltip("��ǰ״̬��ʹ�õ������ļ�")]
        public string fileName = "AI_01.txt";

        //����״̬��
        //Ӳ����
        //private void ConfigFSM()
        //{
        //    states = new List<FSMState>();
        //    //-- ����״̬����
        //    IdleState idle = new IdleState();
        //    //-- ����״̬��AddMap��
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    //-- ����״̬��
        //    states.Add(idle);

        //    DeadState dead = new DeadState();
        //    states.Add(dead);

        //    PursuitState pursuit = new PursuitState();
        //    pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    pursuit.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
        //    pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);
        //    states.Add(pursuit);

        //    AttackingState attacking = new AttackingState();
        //    attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
        //    attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);
        //    states.Add(attacking);

        //    PatrollingState patrolling = new PatrollingState();
        //    patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);
        //    states.Add(patrolling);

        //}
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            //ÿ��״̬������һ����ȡ������
            //var map = new AIConfigurationReader(fileName).Map;
            //ÿ���ļ�����һ����ȡ������
            var map = AIConfigurationReaderFactory.GetMap(fileName);
            foreach (var state in map)
            {
                //state.Key ״̬����
                //state.Value ״̬ӳ��
                Type type = Type.GetType("AI.FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                foreach(var dic in state.Value)
                {
                    //dic.Key �������
                    //dic.Value ״̬���
                    //string -> Enum ʹ��Parse����
                    FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), dic.Key);
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), dic.Value);
                    stateObj.AddMap(triggerID, stateID);
                }

                states.Add(stateObj);
            }
        }

        private void InitDefaultState()
        {
            //����Ĭ��״̬
            defaultState = states.Find(s => s.StateID == defaultStateID);
            //Ϊ��ǰ״̬��ֵ
            currentState = defaultState;
            //����״̬
            currentState.EnterState(this);
        }



        //�л�״̬
        public void ChangeActiveState(FSMStateID stateID)
        {
            //���õ�ǰ״̬
            //�����Ҫ�л���״̬����ǣ�Default����ֱ�ӷ���Ĭ��״̬defaultStateID;
            //�����״̬�б��в���

            //�뿪��һ��״̬
            currentState.ExitState(this);
            currentState = stateID == FSMStateID.Default 
                ? defaultState
                : states.Find(states => states.StateID == stateID);
            //������һ��״̬
            currentState.EnterState(this);
        }
        #endregion

        #region Ϊ״̬�������ṩ�ĳ�Ա
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public MyCharacterStatus chStatus;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("����Ŀ���ǩ")]
        public string[] targetTags = { "Player" };
        [Tooltip("��Ұ����")]
        public float sightDistance = 10;
        //Ѱ·���
        private NavMeshAgent navAgent;
        [Tooltip("�ܲ��ٶ�")]
        public float runSpeed;
        [Tooltip("�߲��ٶ�")]
        public float walkSpeed;
        [Tooltip("·��")]
        public Transform[] wayPoints;
        [Tooltip("Ѳ��ģʽ")]
        public PatrolMode patrolMode;
        [HideInInspector]
        public bool isPatrolComplete;
        [HideInInspector]
        public CharacterSkillSystem skillSystem;

        public void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<MyCharacterStatus>();
            navAgent = GetComponent<NavMeshAgent>();
            skillSystem = GetComponent<CharacterSkillSystem>();
        }

        //����Ŀ��
        private void SearchTarget()
        {
            //
            SkillData skillData = new SkillData()
            {
                attackTargetTags = targetTags,
                attackDistance = sightDistance,
                attackAngle = 360,
                attackType = SkillAttackType.Single
            };
            Transform[] targetArr =  new SectorAttackSelector().SelectTarget(skillData, transform);
            targetTF = targetArr.Length == 0 ? null : targetArr[0];
        }      
        //Ѱ·
        public void MoveToTarget(Vector3 position, float stopDis,float moveSpeed)
        {
            //ͨ��Ѱ·���ʵ��
            navAgent.SetDestination(position);
            navAgent.stoppingDistance = stopDis;
            navAgent.speed = moveSpeed;
        }

        //ֹͣ�ƶ�
        public void StopMove()
        {
            //navAgent.SetDestination(transform.position);
            navAgent.enabled = false;
            navAgent.enabled = true;
        }



        #endregion
    }
}
