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
    public interface IAttackSelector
    {
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="skillData">技能数据</param>
        /// <param name="skillTF">技能所在物体的变换组件</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData skillData,Transform skillTF);
    }
}
