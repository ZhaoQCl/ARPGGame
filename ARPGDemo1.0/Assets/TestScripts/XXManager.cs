using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 
 * 
 */
namespace ns 
{
    /// <summary>
    /// ������ ��1.Ψһ 2.����
    /// </summary>
    public class XXManager : MonoSingleton<XXManager>
    {
        //public static XXManager Instance { get; private set; }

        //public void Awake()
        //{
        //    Instance = this;
        //}

        //ȱ�㣺
        //1. �����ظ�
        //2. ������Awake��ֵ�����пͻ��˴���ֻ����Awake�Ժ�Ľű����������з���

        /*
         �������������MonoSingleton��
        1. �����ԣ������д���Ψһ�Ķ��󣬼����øö���̳е�ǰ��
        2. ���ʹ�ã�
            -- �̳�ʱ���봫���������͡�
            -- ������ű����������У�ͨ���������ͷ���Instance���ԡ�
         */
        public void Fun1()
        {
            print("Fun1");
        }
        


    }
}
