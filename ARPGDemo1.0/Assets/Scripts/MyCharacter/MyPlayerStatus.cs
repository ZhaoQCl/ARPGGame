using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.MyCharacter
{
    public class MyPlayerStatus : MyCharacterStatus
    {
        /// <summary>
        /// ����
        /// </summary>
        /// 
        private new void Start()
        {
            //ͨ�����������õ��ã����Ǹ�����ͬ��������������������
            //��unity�У������฽�ӵ������У�������������൱��ͨ�����������õ��ýű���������
            //����ű��������ڳ�ͻ�����������෽����ʹ��base.���෽����ֹ����
        }
        protected override void Death()
        {
            //������ʱ�޸ĸ��෽�����ַ
            //��Ϊ��Ҫ���ø��ִ࣬�����෽��
            base.Death();
            print("��Ϸ����");    
        }
    }
}
