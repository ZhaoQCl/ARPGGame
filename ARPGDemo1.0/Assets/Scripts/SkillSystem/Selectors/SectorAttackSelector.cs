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
    /// ����/Բ��ѡ��
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            List<Transform> targets = new List<Transform>();
            //��ȡ����Ŀ��
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGoAraay = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                targets.AddRange(tempGoAraay.Select(g => g.transform));
            }


            //�жϹ�����Χ������/Բ�Σ�
            targets = targets.FindAll(t =>
                Vector3.Distance(t.position, skillTF.position) <= skillData.attackDistance &&
                Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= skillData.attackAngle / 2
            );


            //ɸѡ���ŵĽ�ɫ
            targets = targets.FindAll(t => t.GetComponent<MyCharacterStatus>().HP > 0);

            //����Ŀ�꣨����/Ⱥ����
            Transform[] result = targets.ToArray();
            if (skillData.attackType == SkillAttackType.Group || result.Length == 0)
                return result;

            Transform min = result.Min(t => Vector3.Distance(t.position,skillTF.position));
            return new Transform[] { min };
        }
    }
}
