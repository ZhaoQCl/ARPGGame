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
    public class CostSPImpact : IImpactEffect
    {
        public void Execute(SkillDeployer skillDeployer)
        {
            var status = skillDeployer.SkillData.owner.GetComponent<MyCharacterStatus>();

            status.SP -= skillDeployer.SkillData.costSP;

        }
    }
}
