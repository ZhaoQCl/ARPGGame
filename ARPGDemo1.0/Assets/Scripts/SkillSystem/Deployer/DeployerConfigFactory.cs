using ARPGDemo.Skill;
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
    /// �ͷ������ù������ṩ�����ͷ��������㷨����Ĺ���
    /// ���ã�������Ĵ��� �� ʹ�÷���
    /// </summary>
    /// 


    public class DeployerConfigFactory : MonoBehaviour
    {

        private static Dictionary<string, object> cache;

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }

        public static IAttackSelector CreateAttackSelector(SkillData skillData)
        {
            //ѡ��������������
            //ARPGDemo.Skill.+ö����+AttackSelector
            //��������ѡ����ARPGDemo.Skill.SectorAttackSelector
            string classNameSelector = string.Format("ARPGDemo.Skill.{0}AttackSelector", skillData.selectorType);
            return CreateObject<IAttackSelector>(classNameSelector);
        }

        public static IImpactEffect[] CreateImpactEffects(SkillData skillData)
        {
            IImpactEffect[] impacts = new IImpactEffect[skillData.impactType.Length];
            //Ӱ��Ч�������淶��
            //ARPGDemo.Skill.+impactType[?]+Impact
            //�������ķ�����ARPGDemo.Skill.CostSPImpact
            for (int i = 0; i < skillData.impactType.Length; i++)
            {
                string classNameImpact = string.Format("ARPGDemo.Skill.{0}Impact", skillData.impactType[i]);
                impacts[i] = CreateObject<IImpactEffect>(classNameImpact);
            }
            return impacts;
        }

        private static T CreateObject<T>(string className) where T : class
        {
            if(!cache.ContainsKey(className) ) 
            {
                Type type = Type.GetType(className);
                object instance = Activator.CreateInstance(type);
                cache.Add(className, instance);
            }
            return cache[className] as T;
        }
    }
}
