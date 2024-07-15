using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace ARPGDemo.MyCharacter
{
    /// <summary>
    /// ��ɫ״̬��
    /// </summary>
    public class MyCharacterStatus : MonoBehaviour
    {
        [Tooltip("��������")]
        public MyCharacterAnimationParameter chParams;

        [Tooltip("Ѫ��")]
        public float HP;

        [Tooltip("���Ѫ��")]
        public float maxHP;

        [Tooltip("����")]
        public float SP;

        [Tooltip("�����")]
        public float maxSP;

        [Tooltip("����������")]
        public float baseATK;

        [Tooltip("������")]
        public float defence;

        [Tooltip("�������")]
        public float attackInterval;

        [Tooltip("��������")]
        public float attackDistance;

        /// <summary>
        /// ���˷���
        /// </summary>
        /// <param name="val">�ܵ��˺�</param>
        public void Damage(float val)
        {
            val -= defence;
            if (val <= 0) return;
            HP -= val;
            if (HP <= 0) Death();
        }

        /// <summary>
        /// ����
        /// </summary>

        //���ø�������������ִ����������������
        protected virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
        }
    }
}
