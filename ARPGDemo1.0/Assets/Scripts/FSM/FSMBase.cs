using ARPGDemo.MyCharacter;
using ARPGDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

/*
    主要程序处理流程：
    状态机每帧检测当前状态的条件 --> 状态类遍历所有的条件对象 --> 
    如果某个条件达成 --> 状态机切换状态

    详细流程：
    Star
 */
namespace AI.FSM
{
    /// <summary>
    /// 状态机
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        /*
        主要程序处理流程：
        状态机每帧检测当前状态的条件 --> 状态类遍历所有的条件对象 --> 
        如果某个条件达成 --> 状态机切换状态

        详细流程：
        Start
        {
            初始化所需组件
            配置状态机
            {
                创建状态列表
                创建状态对象
                添加映射 --> 添加到状态列表
            }
            初始化默认状态
            {
                根据编辑器默认状态ID设置当前状态并进入
            }
        }
        Update
        {
            检测当前状态条件
            {
                遍历条件列表，检测每个条件是否满足；
                如果发现条件达成，则通过状态机进行切换（设置当前状态）。
            }
            执行当前状态行为
        }
        */
        #region 脚本生命周期
        public void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
        }


        //每帧处理的逻辑
        public void Update()
        {
            //判断条件是否满足.....
            currentState.Reason(this);
            //执行当前状态逻辑.....
            currentState.ActionState(this);
            //查找目标
            SearchTarget();
        }
        #endregion

        #region 状态机自身成员

        //状态列表
        private List<FSMState> states;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;

        //当前状态
        private FSMState currentState;
        //初始化默认状态
        private FSMState defaultState;
        [Tooltip("当前状态机使用的配置文件")]
        public string fileName = "AI_01.txt";

        //配置状态机
        //硬编码
        //private void ConfigFSM()
        //{
        //    states = new List<FSMState>();
        //    //-- 创建状态对象
        //    IdleState idle = new IdleState();
        //    //-- 设置状态（AddMap）
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    //-- 加入状态机
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
            //每个状态机创建一个读取器对象
            //var map = new AIConfigurationReader(fileName).Map;
            //每个文件创建一个读取器对象
            var map = AIConfigurationReaderFactory.GetMap(fileName);
            foreach (var state in map)
            {
                //state.Key 状态名称
                //state.Value 状态映射
                Type type = Type.GetType("AI.FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                foreach(var dic in state.Value)
                {
                    //dic.Key 条件编号
                    //dic.Value 状态编号
                    //string -> Enum 使用Parse函数
                    FSMTriggerID triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), dic.Key);
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), dic.Value);
                    stateObj.AddMap(triggerID, stateID);
                }

                states.Add(stateObj);
            }
        }

        private void InitDefaultState()
        {
            //查找默认状态
            defaultState = states.Find(s => s.StateID == defaultStateID);
            //为当前状态赋值
            currentState = defaultState;
            //进入状态
            currentState.EnterState(this);
        }



        //切换状态
        public void ChangeActiveState(FSMStateID stateID)
        {
            //设置当前状态
            //如果需要切换的状态编号是：Default，则直接返回默认状态defaultStateID;
            //否则从状态列表中查找

            //离开上一个状态
            currentState.ExitState(this);
            currentState = stateID == FSMStateID.Default 
                ? defaultState
                : states.Find(states => states.StateID == stateID);
            //进入下一个状态
            currentState.EnterState(this);
        }
        #endregion

        #region 为状态与条件提供的成员
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public MyCharacterStatus chStatus;
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("攻击目标标签")]
        public string[] targetTags = { "Player" };
        [Tooltip("视野距离")]
        public float sightDistance = 10;
        //寻路组件
        private NavMeshAgent navAgent;
        [Tooltip("跑步速度")]
        public float runSpeed;
        [Tooltip("走步速度")]
        public float walkSpeed;
        [Tooltip("路点")]
        public Transform[] wayPoints;
        [Tooltip("巡逻模式")]
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

        //查找目标
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
        //寻路
        public void MoveToTarget(Vector3 position, float stopDis,float moveSpeed)
        {
            //通过寻路组件实现
            navAgent.SetDestination(position);
            navAgent.stoppingDistance = stopDis;
            navAgent.speed = moveSpeed;
        }

        //停止移动
        public void StopMove()
        {
            //navAgent.SetDestination(transform.position);
            navAgent.enabled = false;
            navAgent.enabled = true;
        }



        #endregion
    }
}
