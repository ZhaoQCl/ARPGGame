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
        /// ����Ŀ��
        /// </summary>
        /// <param name="skillData">��������</param>
        /// <param name="skillTF">������������ı任���</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData skillData,Transform skillTF);
    }
}
