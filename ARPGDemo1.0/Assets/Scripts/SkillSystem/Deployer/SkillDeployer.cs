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
    /// �����ͷ��������ࣩ
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        //�������ݶ���
        private SkillData skillData;

        //ѡ���㷨����
        private IAttackSelector selector;

        //Ӱ���㷨����
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
        
        //��ʼ���ͷ���
        private void InitDeployer()
        {
            //ѡȡ�㷨����
            selector = DeployerConfigFactory.CreateAttackSelector(SkillData);

            //Ӱ���㷨����
            impactArray = DeployerConfigFactory.CreateImpactEffects(SkillData);
        }

        //ִ���㷨����
        //ѡ��
        public void CalculateTargets()
        {
            skillData.attackTargets = selector.SelectTarget(skillData, transform);
        }


        //Ӱ��
        public void ImpactTargets()
        {
            for(int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);
            }
        }

        //�ͷŷ���
        public abstract void DeploySkill();//�����ܹ��������ã�������ʵ�֣������ͷŲ���

    }
}
