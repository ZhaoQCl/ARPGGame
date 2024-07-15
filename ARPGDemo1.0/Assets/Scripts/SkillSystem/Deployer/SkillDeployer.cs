using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 技能释放器（父类）
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        //技能数据对象
        private SkillData skillData;

        //选区算法对象
        private IAttackSelector selector;

        //影响算法对象
        private IImpactEffect[] impactArray;

        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }
        
        //初始化释放器
        private void InitDeployer()
        {
            //选取算法对象
            selector = DeployerConfigFactory.CreateAttackSelector(SkillData);

            //影响算法对象
            impactArray = DeployerConfigFactory.CreateImpactEffects(SkillData);
        }

        //执行算法对象
        //选区
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);
        }


        //影响
        public void ImpactTargets()
        {
            for(int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);
            }
        }

        //释放方法
        public abstract void DeploySkill();//供技能管理器调用，由子类实现，定义释放策略

    }
}
