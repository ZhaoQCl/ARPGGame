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
    /// Ӱ��Ч���ӿ��㷨
    /// </summary>
    public interface IImpactEffect
    {
        //�˺�����    5
        void Execute(SkillDeployer skillDeployer);
    }
}
