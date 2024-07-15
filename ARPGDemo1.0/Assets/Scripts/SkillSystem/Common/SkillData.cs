using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * 
 * 
 */
namespace ARPGDemo.Skill
{
    /// <summary>
    /// ��������
    /// </summary>
    [Serializable]
    public class SkillData
    {
        //����ID
        public int skilllD;
        //��������
        public string name;
        //��������
        public string description;
        //��ȴʱ��
        public int coolTime;
        //��ȴʣ��
        public int coolRemain;
        //ħ������
        public int costSP;
        //��������
        public float attackDistance;
        //�����Ƕ�
        public float attackAngle;
        //����Ŀ��tags
        public string[] attackTargetTags = { "Enemy" };
        //����Ŀ���������
        [HideInInspector]
        public Transform[] attackTargets;
        //����Ӱ������
        public string[] impactType = { "CostSP", "Damage" };
        //��������һ�����ܱ��
        public int nextBatterld;
        //�˺�����
        public float atkRatio;
        //����ʱ��
        public float durationTime;
        //�˺����
        public float atklnterval;
        //��������
        [HideInInspector]
        public GameObject owner;
        //����Ԥ�Ƽ�����
        public string prefabName;
        //Ԥ�Ƽ�����
        [HideInInspector]
        public GameObject skillPrefab;
        //��������
        public string animationName;
        //�ܻ���Ч����
        public string hitFxName;
        //�ܻ���ЧԤ�Ƽ�
        [HideInInspector]
        public GameObject hitFxPrefab; 
        //���ܵȼ�
        public int level;
        //�������͵�����Ⱥ��
        public SkillAttackType attackType;
        //ѡ����������(Բ��)������
        public SelectorType selectorType;


    }
}
