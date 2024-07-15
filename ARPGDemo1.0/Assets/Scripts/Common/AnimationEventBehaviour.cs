using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// �����¼���Ϊ��
    /// </summary>
    public class AnimationEventBehaviour : MonoBehaviour
    {
        //�߻���
        //Ϊ����Ƭ������¼���ָ��OnCancelAnim��OnAttack

        //����
        //�ڽű��в��Ŷ�������������Ҫִ�е��߼���ע��attackHandler�¼�

        private Animator anim;

        public event Action attackHandler;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        //��Unity�������
        private void OnCancelAnim(string animPara)
        {
            anim.SetBool(animPara, false);
        }

        //��Unity�������
        private void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler();//�����¼�
            }
        }
    }

}
