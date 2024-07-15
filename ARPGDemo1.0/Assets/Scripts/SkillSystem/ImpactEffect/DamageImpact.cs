using ARPGDemo.MyCharacter;
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
    /// 
    /// </summary>
    public class DamageImpact : IImpactEffect
    {
        //��Ϊ��Ա�����������ֻ��һ������Ա����Ҳֻ��һ��
        //������ʹ�û����Э�̣���Э�̵��ù����У���Ա�������ݱ��޸ģ�����ԭ��Э�̷��������仯
        //private SkillData skillData;

        public void Execute(SkillDeployer skillDeployer)
        {
            //skillData = skillDeployer.SkillData;
            skillDeployer.StartCoroutine(RepeatDamage(skillDeployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer skillDeployer)
        {
            float atkTime = 0;
            do
            {
                //�˺�Ŀ����������������
                OnceDamage(skillDeployer.SkillData);
                yield return new WaitForSeconds(skillDeployer.SkillData.atklnterval);
                atkTime += skillDeployer.SkillData.atklnterval;
                skillDeployer.CalculateTargets();

            }while(atkTime < skillDeployer.SkillData.durationTime);
        }

        private void OnceDamage(SkillData skillData)
        {
            //ʵ�ʹ��������������� * ����������
            float atk = skillData.atkRatio * skillData.owner.GetComponent<MyCharacterStatus>().baseATK;
            for(int i = 0 ; i < skillData.attackTargets.Length ; i++)
            {
                var status = skillData.attackTargets[i].GetComponent<MyCharacterStatus>();
                status.Damage(atk);
            }
        }
    }
}
