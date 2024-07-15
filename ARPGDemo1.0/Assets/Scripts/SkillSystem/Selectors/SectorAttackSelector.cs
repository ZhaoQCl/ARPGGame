using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo.MyCharacter;
using System.Linq;
/*
* 
* 
*/
namespace ARPGDemo.Skill
{
    /// <summary>
    /// 扇形/圆形选区
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            List<Transform> targets = new List<Transform>();
            //获取所有目标
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGoAraay = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                targets.AddRange(tempGoAraay.Select(g => g.transform));
            }


            //判断攻击范围（扇形/圆形）
            targets = targets.FindAll(t =>
                Vector3.Distance(t.position, skillTF.position) <= skillData.attackDistance &&
                Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= skillData.attackAngle / 2
            );


            //筛选活着的角色
            targets = targets.FindAll(t => t.GetComponent<MyCharacterStatus>().HP > 0);

            //返回目标（单攻/群攻）
            Transform[] result = targets.ToArray();
            if (skillData.attackType == SkillAttackType.Group || result.Length == 0)
                return result;

            Transform min = result.Min(t => Vector3.Distance(t.position,skillTF.position));
            return new Transform[] { min };
        }
    }
}
