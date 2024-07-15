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
        //因为成员变量如果对象只有一个，成员变量也只有一个
        //方法内使用缓存和协程，在协程调用过程中，成员变量数据被修改，导致原先协程发生条件变化
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
                //伤害目标生命。。。。。
                OnceDamage(skillDeployer.SkillData);
                yield return new WaitForSeconds(skillDeployer.SkillData.atklnterval);
                atkTime += skillDeployer.SkillData.atklnterval;
                skillDeployer.CalculateTargets();

            }while(atkTime < skillDeployer.SkillData.durationTime);
        }

        private void OnceDamage(SkillData skillData)
        {
            //实际攻击力：攻击比率 * 基础攻击力
            float atk = skillData.atkRatio * skillData.owner.GetComponent<MyCharacterStatus>().baseATK;
            for(int i = 0 ; i < skillData.attackTargets.Length ; i++)
            {
                var status = skillData.attackTargets[i].GetComponent<MyCharacterStatus>();
                status.Damage(atk);
            }
        }
    }
}
