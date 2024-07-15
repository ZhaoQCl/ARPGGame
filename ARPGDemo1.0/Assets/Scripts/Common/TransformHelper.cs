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
    /// �任���������
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// δ֪�㼶�����Һ��ָ�����Ƶı任���
        /// </summary>
        /// <param name="currentTF">��ǰ�任���</param>
        /// <param name="childNmae">�������</param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            //�ݹ飺�����ڲ������ֵ�������Ĺ��̡�
            //1.���������ֲ���
            Transform childTf = currentTF.Find(childName);
            if (childTf != null) return childTf;
            for(int i = 0; i < currentTF.childCount; i++)
            {
                //2.�����񽻸�������
                childTf = FindChildByName(currentTF.GetChild(i), childName);
                if (childTf != null) return childTf;
            }
            return null;
        }
    }
}
