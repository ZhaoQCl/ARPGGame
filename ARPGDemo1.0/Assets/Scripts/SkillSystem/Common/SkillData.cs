using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 
 * 
 */
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 技能数据
    /// </summary>
    [Serializable]
    public class SkillData
    {
        //技能ID
        public int skilllD;
        //技能名称
        public string name;
        //技能描述
        public string description;
        //冷却时间
        public int coolTime;
        //冷却剩余
        public int coolRemain;
        //魔法消耗
        public int costSP;
        //攻击距离
        public float attackDistance;
        //攻击角度
        public float attackAngle;
        //攻击目标tags
        public string[] attackTargetTags = { "Enemy" };
        //攻击目标对象数组
        [HideInInspector]
        public Transform[] attackTargets;
        //技能影响类型
        public string[] impactType = { "CostSP", "Damage" };
        //连击的下一个技能编号
        public int nextBatterld;
        //伤害比率
        public float atkRatio;
        //持续时间
        public float durationTime;
        //伤害间隔
        public float atklnterval;
        //技能所属
        [HideInInspector]
        public GameObject owner;
        //技能预制件名称
        public string prefabName;
        //预制件对象
        [HideInInspector]
        public GameObject skillPrefab;
        //动画名称
        public string animationName;
        //受击特效名称
        public string hitFxName;
        //受击特效预制件
        [HideInInspector]
        public GameObject hitFxPrefab; 
        //技能等级
        public int level;
        //攻击类型单攻，群攻
        public SkillAttackType attackType;
        //选择类型扇形(圆形)，矩形
        public SelectorType selectorType;


    }
}
